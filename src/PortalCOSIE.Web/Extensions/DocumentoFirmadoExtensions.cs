using PortalCOSIE.Application.Features.Tramites.DTO;

namespace PortalCOSIE.Web.Extensions
{
    public static class DocumentoFirmadoExtensions
    {
        public static DocumentoFirmadoDTO? ToDocumentoFirmado(this IFormFile? archivo, string? firmaBase64)
        {
            if (archivo == null || archivo.Length == 0)
                return null;

            if (archivo.Length > 3 * 1024 * 1024)
                throw new ArgumentException("El archivo excede el tamaño máximo permitido de 3 MB.");

            if (string.IsNullOrWhiteSpace(firmaBase64))
                throw new ArgumentException($"La firma CMS del archivo '{archivo.FileName}' es obligatoria.");

            return new DocumentoFirmadoDTO
            {
                Nombre = archivo.FileName,
                Contenido = archivo.OpenReadStream(),
                FirmaCms = Convert.FromBase64String(firmaBase64)
            };
        }
    }
}