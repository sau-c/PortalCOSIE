using PortalCOSIE.Domain.Entities.Documentos;

namespace PortalCOSIE.Domain.Entities.Tramites
{
    public interface IDocumentoRepository
    {
        Task SubirArchivoAsync(Documento documento, Stream contenido);
    }
}
