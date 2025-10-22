using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ICatalogoService
    {
        //EstadoTramite CRUD
        Task<IEnumerable<EstadoTramite>> ListarEstados();
        Task EliminarEstado(int id);
        Task EditarEstado(EstadoTramite estadoTramite);
        Task CrearEstado(EstadoTramite estadoTramite);

        //EstadoDocumento CRUD
        Task<IEnumerable<EstadoDocumento>> ListarEstadosDocumento();
        Task EliminarEstadoDocumento(int id);
        Task EditarEstadoDocumento(EstadoDocumento estadoDocumento);
        Task CrearEstadoDocumento(EstadoDocumento estadoDocumento);

        //EstadoDocumento CRUD
        Task<IEnumerable<TipoTramite>> ListarTipoTramite();
        Task EliminarTipoTramite(int id);
        Task EditarTipoTramite(TipoTramite tipoTramite);
        Task CrearTipoTramite(TipoTramite tipoTramite);

        Task<PeriodoConfig> ListarConfiguracionPeriodos();
    }
}