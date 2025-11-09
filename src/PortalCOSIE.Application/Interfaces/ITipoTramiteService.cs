using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ITipoTramiteService
    {
        Task<IEnumerable<TipoTramite>> ListarTipoTramiteActivos();
        Task<IEnumerable<TipoTramite>> ListarTipoTramite();
        Task CrearTipoTramite(string nombre);
        Task EditarTipoTramite(int id, string nombre);
        Task ToggleTipoTramite(int id);
    }
}
