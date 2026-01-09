using Azure.Storage.Blobs;
using PortalCOSIE.Application.Services.Storage;

public class AzureStorageService : IStorageService
{
    private readonly BlobContainerClient _blobContainerClient;

    public AzureStorageService(
        BlobContainerClient blobContainerClient
        )
    {
        _blobContainerClient = blobContainerClient;
    }

    public async Task<string> UploadAsync(Stream contenido, string nombre)
    {
        // Generamos un nombre único para evitar colisiones
        var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(nombre)}";
        var blobClient = _blobContainerClient.GetBlobClient(uniqueName);

        // Subimos a Azure
        await blobClient.UploadAsync(contenido, overwrite: true);

        return uniqueName;
    }
    public async Task<Stream> DownloadAsync(string ruta)
    {
        BlobClient blobClient = _blobContainerClient.GetBlobClient(ruta);
        if (!await blobClient.ExistsAsync())
            throw new FileNotFoundException("El documento no existe en el almacenamiento de blobs.");

        var downloadInfo = await blobClient.DownloadAsync();
        return downloadInfo.Value.Content;
    }
    public async Task DeleteAsync(string blobPath)
    {
        BlobClient blobClient = _blobContainerClient.GetBlobClient(blobPath);
        if (!await blobClient.ExistsAsync())
            throw new FileNotFoundException("El documento no existe en el almacenamiento de blobs.");
        await blobClient.DeleteIfExistsAsync();
    }
}