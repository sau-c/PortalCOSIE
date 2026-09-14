using PortalCOSIE.Application.Features.Tramites.DTO;
using PortalCOSIE.Application.Features.Tramites.Services;
using PortalCOSIE.Application.Services.Query;
using PortalCOSIE.Application.Services.Storage;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Application.Features.Tramites.Queries.DescargarDocumento
{
    public class DescargarDocumentoHandler : IRequestHandler<DescargarDocumentoQuery, ArchivoDTO>
    {
        private readonly IStorageService _storageService;
        private readonly IUsuarioQueryService _usuarioQueryService;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly AcusePdfPublicacionService _acusePublicacion;

        public DescargarDocumentoHandler(
            IStorageService storageService,
            IUsuarioQueryService usuarioQueryService,
            IUsuarioRepository usuarioRepo,
            AcusePdfPublicacionService acusePublicacion)
        {
            _storageService = storageService;
            _usuarioQueryService = usuarioQueryService;
            _usuarioRepo = usuarioRepo;
            _acusePublicacion = acusePublicacion;
        }

        public async Task<ArchivoDTO> Handle(DescargarDocumentoQuery query)
        {
            Documento documento = await _usuarioQueryService.ObtenerDatosDocumentoPorId(query.DocumentoId);

            if (documento == null)
                throw new ApplicationException("Documento no encontrado.");

            bool tieneAcceso = false;

            if (query.Rol == "Administrador")
            {
                tieneAcceso = true;
            }
            else if (query.Rol == "Alumno")
            {
                var alumno = await _usuarioRepo.BuscarUsuario(query.IdentityUserId);
                if (alumno != null && documento.Tramite != null && documento.Tramite.PerteneceAAlumno(alumno.Id))
                    tieneAcceso = true;
            }
            else if (query.Rol == "Personal")
            {
                var personal = await _usuarioRepo.BuscarPersonal(query.IdentityUserId);
                if (personal != null && documento.Tramite != null && documento.Tramite.PuedeSerAtendidoPor(personal.Id))
                    tieneAcceso = true;
            }

            if (!tieneAcceso)
                throw new ApplicationException("No tienes acceso a visualizar este documento.");

            await using var stream = await _storageService.DownloadAsync(documento.Ruta);
            Stream contenidoEntrega;

            if (AcusePdfPublicacionService.RequierePanelPublico(documento))
            {
                var pdfOriginal = await LeerBytesAsync(stream);
                var pdfPublico = await _acusePublicacion.AplicarPanelPublicoAsync(
                    documento,
                    pdfOriginal,
                    query.BaseUrl);
                contenidoEntrega = new MemoryStream(pdfPublico);
            }
            else
            {
                contenidoEntrega = new MemoryStream();
                await stream.CopyToAsync(contenidoEntrega);
                contenidoEntrega.Position = 0;
            }

            string nombreArchivo = !string.IsNullOrWhiteSpace(documento.Nombre)
                ? documento.Nombre
                : Path.GetFileName(documento.Ruta);

            return new ArchivoDTO
            {
                Nombre = nombreArchivo,
                Contenido = contenidoEntrega,
                ContentType = "application/pdf"
            };
        }

        private static async Task<byte[]> LeerBytesAsync(Stream stream)
        {
            if (stream.CanSeek)
                stream.Position = 0;

            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}