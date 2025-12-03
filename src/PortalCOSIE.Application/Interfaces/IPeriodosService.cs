using PortalCOSIE.Application.DTO.Periodo;
using PortalCOSIE.Domain.Entities.Calendario;

namespace PortalCOSIE.Application.Interfaces
{
    public interface IPeriodosService
    {
        #region PERIODO_CONFIG
        Task<IEnumerable<string>> ListarPeriodos();
        Task<PeriodoConfigDTO> BuscarPeriodoConfig();
        Task EditarPeriodoConfig(PeriodoConfigDTO dto);
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