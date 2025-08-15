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
    }
}