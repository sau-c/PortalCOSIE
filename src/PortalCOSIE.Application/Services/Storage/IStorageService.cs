namespace PortalCOSIE.Application.Services.Storage
{
    public interface IStorageService
    {
        Task<string> UploadAsync(Stream contenido, string nombre);
        Task<Stream> DownloadAsync(string ruta);
        Task DeleteAsync(string ruta);
    }
}