using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ICatalogoService
    {
        #region ESTADO_TRAMITE
        Task<IEnumerable<EstadoTramite>> ListarEstadosTramite();
        Task<IEnumerable<EstadoTramite>> ListarTodoEstadoTramite();
        Task CrearEstadoTramite(string nombre);
        Task EditarEstadoTramite(int id, string nombre);
        Task ToggleEstadoTramite(int id);
        #endregion

        #region ESTADO_DOCUMENTO
        Task<IEnumerable<EstadoDocumento>> ListarEstadosDocumento();
        Task<IEnumerable<EstadoDocumento>> ListarTodoEstadosDocumento();
        Task CrearEstadoDocumento(string nombre);
        Task EditarEstadoDocumento(int id, string nombre);
        Task ToggleEstadoDocumento(int id);
        #endregion

        #region TIPO_TRAMITE
        Task<IEnumerable<TipoTramite>> ListarTipoTramite();
        Task<IEnumerable<TipoTramite>> ListarTodoTipoTramite();
        Task CrearTipoTramite(string nombre);
        Task EditarTipoTramite(int id, string nombre);
        Task ToggleTipoTramite(int id);
        #endregion

        Task<PeriodoConfig> ListarPeriodoConfig();
        Task<IEnumerable<string>> ListarPeriodos();
        Task EditarPeriodoConfig(int anioInicio, int periodoInicio, int anioFin, int periodoFin);
        Task<IEnumerable<SesionCOSIE>> ListarSesiones();
    }
}