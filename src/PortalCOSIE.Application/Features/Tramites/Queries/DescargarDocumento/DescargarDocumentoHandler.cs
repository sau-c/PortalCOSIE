using PortalCOSIE.Application.Features.Tramites.DTO;
using PortalCOSIE.Application.Services;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Application.Features.Tramites.Queries.DescargarDocumento
{
    public class DescargarDocumentoHandler : IRequestHandler<DescargarDocumentoQuery, ArchivoDTO>
    {
        private readonly IStorageService _storageService;
        private readonly IUsuarioQueryService _usuarioQueryService;
        private readonly IUsuarioRepository _usuarioRepo;

        public DescargarDocumentoHandler(
            IStorageService storageService,
            IUsuarioQueryService usuarioQueryService,
            IUsuarioRepository usuarioRepo)
        {
            _storageService = storageService;
            _usuarioQueryService = usuarioQueryService;
            _usuarioRepo = usuarioRepo;
        }

        public async Task<ArchivoDTO> Handle(DescargarDocumentoQuery query)
        {
            // 1. Obtención de datos
            // Nota: Asegúrate de que este método traiga el Tramite relacionado (Include)
            Documento documento = await _usuarioQueryService.ObtenerDatosDocumentoPorId(query.DocumentoId);

            if (documento == null)
                throw new ApplicationException("Documento no encontrado.");

            // 2. Validación de acceso unificada
            bool tieneAcceso = false;

            if (query.Rol == "Administrador")
            {
                tieneAcceso = true;
            }
            else if (query.Rol == "Alumno")
            {
                var alumno = await _usuarioRepo.BuscarUsuario(query.IdentityUserId);
                // Validamos null check en documento.Tramite para evitar NullReferenceException
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

            // 3. Descarga del Stream
            // IMPORTANTE: Aquí NO usamos 'using'. El stream debe devolverse abierto 
            // para que el Controller lo envíe al navegador.
            var stream = await _storageService.DownloadAsync(documento.BlobPath);

            // 4. Determinar nombre y tipo
            string nombreArchivo = !string.IsNullOrWhiteSpace(documento.Nombre)
                ? documento.Nombre
                : Path.GetFileName(documento.BlobPath);

            return new ArchivoDTO
            {
                Nombre = nombreArchivo,
                Contenido = stream,
                ContentType = "application/pdf"
            };
        }
    }
}