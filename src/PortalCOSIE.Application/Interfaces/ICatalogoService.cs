using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Entities.Calendario;
using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ICatalogoService
    {
        #region ESTADO_TRAMITE
        Task<IEnumerable<EstadoTramite>> ListarEstadosTramiteActivos();
        Task<IEnumerable<EstadoTramite>> ListarEstadoTramite();
        Task CrearEstadoTramite(string nombre);
        Task EditarEstadoTramite(int id, string nombre);
        Task ToggleEstadoTramite(int id);
        #endregion

        #region ESTADO_DOCUMENTO
        Task<IEnumerable<EstadoDocumento>> ListarEstadosDocumentoActivos();
        Task<IEnumerable<EstadoDocumento>> ListarEstadosDocumento();
        Task CrearEstadoDocumento(string nombre);
        Task EditarEstadoDocumento(int id, string nombre);
        Task ToggleEstadoDocumento(int id);
        #endregion

        #region TIPO_TRAMITE
        Task<IEnumerable<TipoTramite>> ListarTipoTramiteActivos();
        Task<IEnumerable<TipoTramite>> ListarTipoTramite();
        Task CrearTipoTramite(string nombre);
        Task EditarTipoTramite(int id, string nombre);
        Task ToggleTipoTramite(int id);
        #endregion

        #region PERIODO_CONFIG
        Task<PeriodoConfig> BuscarPeriodoConfig();
        Task<IEnumerable<string>> ListarPeriodos();
        Task EditarPeriodoConfig(int anioInicio, int periodoInicio, int anioFin, int periodoFin);
        #endregion

        Task<IEnumerable<SesionCOSIE>> ListarSesiones();
        Task<IEnumerable<SesionCOSIE>> ListarSesionesActivas();
        Task CrearSesion(string numeroSesion, DateTime fechaSesion, List<DateTime> fechasRecepcion);
        Task EditarSesion(int id, string numeroSesion, DateTime fechaSesion, List<DateTime> fechasRecepcion);
        Task ToggleSesion(int id);
    }
}