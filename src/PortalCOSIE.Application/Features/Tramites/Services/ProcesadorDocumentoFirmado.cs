using PortalCOSIE.Application.Features.Tramites.DTO;
using PortalCOSIE.Application.Services.Crypto;
using PortalCOSIE.Application.Services.Storage;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Application.Features.Tramites.Services
{
    public class ProcesadorDocumentoFirmado
    {
        private readonly IFirmaVerificacionService _firmaVerificacionService;
        private readonly IStorageService _storageService;

        public ProcesadorDocumentoFirmado(
            IFirmaVerificacionService firmaVerificacionService,
            IStorageService storageService)
        {
            _firmaVerificacionService = firmaVerificacionService;
            _storageService = storageService;
        }

        public async Task<Documento> CrearAsync(
            DocumentoFirmadoDTO documento,
            TipoDocumento tipo,
            int tramiteId,
            int estadoDocumentoId,
            Certificado certificado)
        {
            ValidarEntrada(documento, tipo);
            ValidarCertificado(certificado);

            byte[] archivoBytes = await LeerArchivoAsync(documento);
            var firmaElectronica = VerificarYCrearFirma(archivoBytes, documento.FirmaCms, certificado);

            string blobPath;
            using (var streamSubida = new MemoryStream(archivoBytes))
            {
                blobPath = await _storageService.UploadAsync(streamSubida, documento.Nombre);
            }

            return new Documento(
                documento.Nombre,
                blobPath,
                tramiteId,
                estadoDocumentoId,
                tipo.Id,
                firmaElectronica);
        }

        public async Task ReemplazarAsync(
            Documento documentoExistente,
            DocumentoFirmadoDTO documento,
            TipoDocumento tipo,
            Certificado certificado)
        {
            ValidarEntrada(documento, tipo);
            ValidarCertificado(certificado);

            byte[] archivoBytes = await LeerArchivoAsync(documento);
            var firmaElectronica = VerificarYCrearFirma(archivoBytes, documento.FirmaCms, certificado);

            using (var streamSubida = new MemoryStream(archivoBytes))
            {
                await _storageService.ReplaceAsync(streamSubida, documentoExistente.Ruta);
            }

            documentoExistente.ActualizarDocumento(documento.Nombre, firmaElectronica);
        }

        private FirmaElectronica VerificarYCrearFirma(byte[] archivoBytes, byte[] firmaCms, Certificado certificado)
        {
            using var streamDocumento = new MemoryStream(archivoBytes);
            using var streamCertificado = new MemoryStream(certificado.CertificadoDer);

            var verificacion = _firmaVerificacionService.VerificarFirmaCms(
                streamDocumento,
                firmaCms,
                streamCertificado);

            if (!verificacion.Succeeded)
                throw new InvalidOperationException(
                    verificacion.Errors.FirstOrDefault() ?? "No se pudo verificar la firma del documento.");

            return new FirmaElectronica(
                firmaCms,
                IFirmaVerificacionService.AlgoritmoCmsPkcs7,
                certificado);
        }

        private void ValidarCertificado(Certificado certificado)
        {
            var vigencia = _firmaVerificacionService.ValidarVigenciaCertificado(certificado);
            if (!vigencia.Succeeded)
                throw new InvalidOperationException(
                    vigencia.Errors.FirstOrDefault() ?? "El certificado del firmante no está vigente.");
        }

        private static void ValidarEntrada(DocumentoFirmadoDTO documento, TipoDocumento tipo)
        {
            if (documento?.Contenido == null)
                throw new InvalidOperationException($"El documento {tipo.Nombre} es requerido.");
            if (documento.FirmaCms == null || documento.FirmaCms.Length == 0)
                throw new InvalidOperationException($"La firma CMS del documento {tipo.Nombre} es requerida.");
        }

        private static async Task<byte[]> LeerArchivoAsync(DocumentoFirmadoDTO documento)
        {
            using var ms = new MemoryStream();
            await documento.Contenido.CopyToAsync(ms);
            return ms.ToArray();
        }
    }
}