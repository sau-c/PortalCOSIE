using PortalCOSIE.Domain.Entities.Documentos;

namespace PortalCOSIE.Application.Services
{
    public interface IStorageService
    {
        //Task<DocumentoDTO?> ObtenerBytesParaDescarga(int tramiteId, int documentoId, string identityUserId);
        Task<string> UploadAsync(Stream contenido, string nombre);
        Task<Stream> DownloadAsync(string ruta);
    }
}