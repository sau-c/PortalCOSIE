using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ITramiteService
    {
        Task<IEnumerable<Tramite>> ListarTodos();
        Task<Tramite?> BuscarPorId(int id);
        Task<Tramite> Crear(Tramite tramite);
        Task Actualizar(Tramite tramite);
        Task Eliminar(int id);

        //TramiteEstado CRUD
        Task<IEnumerable<TramiteEstado>> ListarEstados();
        Task EliminarEstado(int id);
        Task EditarEstado(TramiteEstado tramiteEstado);
        Task CrearEstado(TramiteEstado tramiteEstado);

        //DocumentoEstado CRUD
        Task<IEnumerable<DocumentoEstado>> ListarEstadosDocumento();
        Task EliminarEstadoDocumento(int id);
        Task EditarEstadoDocumento(DocumentoEstado documentoEstado);
        Task CrearEstadoDocumento(DocumentoEstado documentoEstado);

        //DocumentoEstado CRUD
        Task<IEnumerable<TipoTramite>> ListarTipoTramite();
        Task EliminarTipoTramite(int id);
        Task EditarTipoTramite(TipoTramite tipoTramite);
        Task CrearTipoTramite(TipoTramite tipoTramite);
    }
}