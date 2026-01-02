using PortalCOSIE.Application.Features.Tramites.DTO;

namespace PortalCOSIE.Application.Interfaces
{
    public interface IDocumentoQueryService
    {
        Task<ArchivoDescargaDTO?> ObtenerBytesParaDescarga(int tramiteId, int documentoId, string identityUserId);
    }
}