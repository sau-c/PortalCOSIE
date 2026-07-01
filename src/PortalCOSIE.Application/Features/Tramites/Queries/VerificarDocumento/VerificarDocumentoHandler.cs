using PortalCOSIE.Application.Services.Crypto;
using PortalCOSIE.Application.Services.Query;
using PortalCOSIE.Application.Services.Storage;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Application.Features.Tramites.Queries.VerificarDocumento
{
    public class VerificarDocumentoHandler : IRequestHandler<VerificarDocumentoQuery, Result<string>>
    {
        private readonly IStorageService _storageService;
        private readonly IUsuarioQueryService _usuarioQueryService;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IFirmaVerificacionService _firmaVerificacionService;

        public VerificarDocumentoHandler(
            IStorageService storageService,
            IUsuarioQueryService usuarioQueryService,
            IUsuarioRepository usuarioRepo,
            IFirmaVerificacionService firmaVerificacionService)
        {
            _storageService = storageService;
            _usuarioQueryService = usuarioQueryService;
            _usuarioRepo = usuarioRepo;
            _firmaVerificacionService = firmaVerificacionService;
        }

        public async Task<Result<string>> Handle(VerificarDocumentoQuery query)
        {
            Documento? documento = await _usuarioQueryService.ObtenerDatosDocumentoPorId(query.DocumentoId);
            if (documento == null)
                return Result<string>.Failure("Documento no encontrado.");

            if (!await TieneAccesoAsync(query, documento))
                return Result<string>.Failure("No tienes permisos para verificar este documento.");

            if (documento.FirmaElectronica?.Certificado == null)
                return Result<string>.Failure("El documento no tiene una firma electrónica registrada.");

            var certificado = documento.FirmaElectronica.Certificado;
            var vigencia = _firmaVerificacionService.ValidarVigenciaCertificado(certificado);
            if (!vigencia.Succeeded)
                return Result<string>.Failure(vigencia.Errors.FirstOrDefault() ?? "El certificado no está vigente.");

            await using var contenido = await _storageService.DownloadAsync(documento.Ruta);
            using var certificadoStream = new MemoryStream(certificado.CertificadoDer);

            var verificacion = _firmaVerificacionService.VerificarFirmaCms(
                contenido,
                documento.FirmaElectronica.FirmaCms,
                certificadoStream);

            if (!verificacion.Succeeded)
                return Result<string>.Failure(verificacion.Errors.FirstOrDefault() ?? "La firma no es válida.");

            return Result<string>.Success(
                $"Firma válida.\n\nFirmante: {certificado.Sujeto}.\n\nVigente hasta {certificado.VigenteHasta:dd/MM/yyyy}.");
        }

        private async Task<bool> TieneAccesoAsync(VerificarDocumentoQuery query, Documento documento)
        {
            if (query.Rol == "Administrador")
                return true;

            if (documento.Tramite == null)
                return false;

            if (query.Rol == "Alumno")
            {
                var alumno = await _usuarioRepo.BuscarUsuario(query.IdentityUserId);
                return alumno != null && documento.Tramite.PerteneceAAlumno(alumno.Id);
            }

            if (query.Rol == "Personal")
            {
                var personal = await _usuarioRepo.BuscarPersonal(query.IdentityUserId);
                return personal != null && documento.Tramite.PuedeSerAtendidoPor(personal.Id);
            }

            return false;
        }
    }
}