using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Entities.Calendario;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ICatalogoService
    {
        #region PERIODO_CONFIG
        Task<PeriodoConfig> BuscarPeriodoConfig();
        Task<IEnumerable<string>> ListarPeriodos();
        Task EditarPeriodoConfig(int anioInicio, int periodoInicio, int anioFin, int periodoFin);
        #endregion

        #region SESION_COSIE
        Task<IEnumerable<SesionCOSIE>> ListarSesiones();
        Task<IEnumerable<SesionCOSIE>> ListarSesionesActivas();
        Task CrearSesion(string numeroSesion, DateTime fechaSesion, List<DateTime> fechasRecepcion);
        Task EditarSesion(int id, string numeroSesion, DateTime fechaSesion, List<DateTime> fechasRecepcion);
        Task ToggleSesion(int id);
        #endregion
    }
}