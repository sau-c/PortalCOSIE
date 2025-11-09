using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Application.Interfaces
{
    public interface IEstadoTramiteService
    {
        Task<IEnumerable<EstadoTramite>> ListarEstadosTramiteActivos();
        Task<IEnumerable<EstadoTramite>> ListarEstadoTramite();
        Task CrearEstadoTramite(string nombre);
        Task EditarEstadoTramite(int id, string nombre);
        Task ToggleEstadoTramite(int id);
    }
}
