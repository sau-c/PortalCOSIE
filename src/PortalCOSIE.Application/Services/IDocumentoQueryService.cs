using PortalCOSIE.Application.Features.Tramites.DTO;

namespace PortalCOSIE.Application.Services
{
    public interface IDocumentoQueryService
    {
        Task<ArchivoDescargaDTO?> ObtenerBytesParaDescarga(int tramiteId, int documentoId, string identityUserId);
    }
}