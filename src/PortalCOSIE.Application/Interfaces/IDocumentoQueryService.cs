using PortalCOSIE.Application.DTO.Tramites;

namespace PortalCOSIE.Application.Interfaces
{
    public interface IDocumentoQueryService
    {
        // Este método está optimizado para traer SOLO los bytes
        // No hidrata entidades, va directo a la base de datos (SQL puro o EF Core proyecciones)
        Task<ArchivoDescargaDTO?> ObtenerBytesParaDescarga(int tramiteId, int documentoId, string identityUserId);
    }
}