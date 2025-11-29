using PortalCOSIE.Domain.Entities.Calendario;

namespace PortalCOSIE.Application.Interfaces
{
    public interface IPeriodosService
    {
        #region PERIODO_CONFIG
        Task<IEnumerable<string>> ListarPeriodos();
        Task<PeriodoConfig> BuscarPeriodoConfig();
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