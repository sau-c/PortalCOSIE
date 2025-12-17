using PortalCOSIE.Application.DTO.Tramites;
using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ITramiteService
    {
        Task<IEnumerable<Tramite>> ListarTodos(string rol, string identityUserId);
        Task SolicitarCTCE(SolicitudCtceDTO dto, string userId);
        Task<TramiteCTCE?> BuscarTramiteCTCEPorId(int tramiteId, string identityUserId);
        Task AsignarPersonal(int tramiteId, string identityUserId);
        Task<ArchivoDescargaDTO> ObtenerDocumentoPorId(int documentoId, string identityUserId);
    }
}