using PortalCOSIE.Domain.Entities.Documentos;

namespace PortalCOSIE.Application.Services
{
    public interface IStorageService
    {
        Task<string> UploadAsync(Stream contenido, string nombre);
        Task<Stream> DownloadAsync(string ruta);
        Task DeleteAsync(string ruta);
    }
}