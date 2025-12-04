using PortalCOSIE.Domain.Entities.Documentos;

namespace PortalCOSIE.Domain.Entities.Tramites
{
    /// <summary>
    /// Define operaciones específicas para la gestión de documentos asociados a trámites.
    /// Se enfoca en el almacenamiento y manipulación de archivos.
    /// </summary>
    public interface IDocumentoRepository
    {
        /// <summary>
        /// Sube un archivo al sistema de almacenamiento y asocia la entidad documento.
        /// </summary>
        Task SubirArchivoAsync(Documento documento, Stream contenido);
    }
}