using PortalCOSIE.Application.Features.Tramites.DTO;
using PortalCOSIE.Application.Features.Tramites.Services;
using PortalCOSIE.Application.Services.Storage;
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
        private readonly AcusePdfPublicacionService _acusePublicacion;

        public DescargarDocumentosPorTramiteHandler(
            IUsuarioRepository usuarioRepo,
            IStorageService storageService,
            ITramiteRepository tramiteRepo,
            AcusePdfPublicacionService acusePublicacion)
        {
            _usuarioRepo = usuarioRepo;
            _storageService = storageService;
            _tramiteRepo = tramiteRepo;
            _acusePublicacion = acusePublicacion;
        }

        public async Task<ArchivoDTO> Handle(DescargarDocumentosPorTramiteQuery query)
        {
            Tramite tramite = await _tramiteRepo.ObtenerTramiteCTCEPorId(query.TramiteId);

            if (tramite == null)
                throw new ApplicationException("Trámite no encontrado.");

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

            if (tramite.Documentos == null || !tramite.Documentos.Any())
                throw new ApplicationException("El trámite no tiene documentos adjuntos.");

            return await Descargar(tramite, query.BaseUrl);
        }

        private async Task<ArchivoDTO> Descargar(Tramite tramite, string baseUrl)
        {
            var zipStream = new MemoryStream();

            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, leaveOpen: true))
            {
                var nombresUsados = new HashSet<string>();

                foreach (var documento in tramite.Documentos)
                {
                    await using var fileStream = await _storageService.DownloadAsync(documento.Ruta);
                    byte[] contenido;

                    if (AcusePdfPublicacionService.RequierePanelPublico(documento))
                    {
                        var pdfOriginal = await LeerBytesAsync(fileStream);
                        contenido = await _acusePublicacion.AplicarPanelPublicoAsync(
                            documento,
                            pdfOriginal,
                            baseUrl,
                            tramite);
                    }
                    else
                    {
                        contenido = await LeerBytesAsync(fileStream);
                    }

                    string nombreArchivo = ObtenerNombreUnico(documento, nombresUsados);
                    nombresUsados.Add(nombreArchivo);

                    var entry = archive.CreateEntry(nombreArchivo, CompressionLevel.Fastest);
                    await using var entryStream = entry.Open();
                    await entryStream.WriteAsync(contenido);
                }
            }

            zipStream.Position = 0;

            return new ArchivoDTO
            {
                Nombre = $"Expediente_{DateTime.Now:yyyyMMdd_HHmm}.zip",
                Contenido = zipStream,
                ContentType = "application/zip"
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

        private string ObtenerNombreUnico(Documento doc, HashSet<string> usados)
        {
            string baseName = !string.IsNullOrWhiteSpace(doc.Nombre)
                ? doc.Nombre
                : Path.GetFileName(doc.Ruta);

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