using PortalCOSIE.Application.Features.Tramites.DTO;
using PortalCOSIE.Application.Services;
using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities.Usuarios;
using System.IO.Compression;

namespace PortalCOSIE.Application.Features.Tramites.Queries.DescargarDocumentosPorTramite
{
    public class DescargarDocumentosPorTramiteHandler : IRequestHandler<DescargarDocumentosPorTramiteQuery, ArchivoDTO>
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly ITramiteRepository _tramiteRepo;
        private readonly IStorageService _storageService;

        public DescargarDocumentosPorTramiteHandler(
            IUsuarioRepository usuarioRepo,
            IStorageService storageService,
            ITramiteRepository tramiteRepo)
        {
            _usuarioRepo = usuarioRepo;
            _storageService = storageService;
            _tramiteRepo = tramiteRepo;
        }

        public async Task<ArchivoDTO> Handle(DescargarDocumentosPorTramiteQuery query)
        {
            Tramite tramite = await _tramiteRepo.ObtenerTramiteCTCEPorId(query.TramiteId);

            if (tramite == null)
                throw new ApplicationException("Trámite no encontrado.");

            // 1. Validación de permisos
            bool tieneAcceso = false;

            if (query.Rol == "Administrador")
            {
                tieneAcceso = true;
            }
            else if (query.Rol == "Alumno")
            {
                var alumno = await _usuarioRepo.BuscarUsuario(query.IdentityUserId);
                if (alumno != null && tramite.PerteneceAAlumno(alumno.Id))
                    tieneAcceso = true;
            }
            else if (query.Rol == "Personal")
            {
                var personal = await _usuarioRepo.BuscarPersonal(query.IdentityUserId);
                if (personal != null && tramite.PuedeSerAtendidoPor(personal.Id))
                    tieneAcceso = true;
            }

            if (!tieneAcceso)
                throw new ApplicationException("No tienes acceso a este trámite.");

            // 2. Verificamos si hay documentos antes de intentar descargar
            if (tramite.Documentos == null || !tramite.Documentos.Any())
                throw new ApplicationException("El trámite no tiene documentos adjuntos.");

            // 3. Pasamos la lista de documentos del trámite
            return await Descargar(tramite.Documentos.ToList());
        }

        private async Task<ArchivoDTO> Descargar(List<Documento> documentos)
        {
            var zipStream = new MemoryStream();

            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, leaveOpen: true))
            {
                // HashSet para evitar nombres duplicados dentro del ZIP
                var nombresUsados = new HashSet<string>();

                foreach (var documento in documentos)
                {
                    using (var fileStream = await _storageService.DownloadAsync(documento.Ruta))
                    {
                        // 1. Calculamos nombre único
                        string nombreArchivo = ObtenerNombreUnico(documento, nombresUsados);
                        nombresUsados.Add(nombreArchivo);

                        // 2. Creamos la entrada en el ZIP
                        var entry = archive.CreateEntry(nombreArchivo, CompressionLevel.Fastest);

                        // 3. Copiamos el stream de Azure al stream del ZIP
                        using (var entryStream = entry.Open())
                        {
                            await fileStream.CopyToAsync(entryStream);
                        }
                    }
                }
            }

            // 6. Rebobinamos el stream del ZIP para que el controlador pueda leerlo desde el principio
            zipStream.Position = 0;

            return new ArchivoDTO
            {
                Nombre = $"Expediente_{DateTime.Now:yyyyMMdd_HHmm}.zip",
                Contenido = zipStream,
                ContentType = "application/zip" // Es buena práctica devolver el ContentType
            };
        }

        private string ObtenerNombreUnico(Documento doc, HashSet<string> usados)
        {
            // Intentamos usar el nombre amigable, si no el del blob
            string baseName = !string.IsNullOrWhiteSpace(doc.Nombre)
                ? doc.Nombre
                : Path.GetFileName(doc.Ruta);

            // Aseguramos extensión
            if (!Path.HasExtension(baseName)) baseName += ".pdf";

            string nombreFinal = baseName;
            int contador = 1;

            while (usados.Contains(nombreFinal))
            {
                string nombreSinExt = Path.GetFileNameWithoutExtension(baseName);
                string ext = Path.GetExtension(baseName);
                nombreFinal = $"{nombreSinExt}_{contador}{ext}";
                contador++;
            }

            return nombreFinal;
        }
    }
}