using PortalCOSIE.Application.DTO.Tramites;
using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ITramiteService
    {
        Task<IEnumerable<Tramite>> ListarTodos();
        Task SolicitarCTCE(SolicitudCtceDTO dto, string userId);
        Task<Tramite?> BuscarPorId(int id);
    }
}