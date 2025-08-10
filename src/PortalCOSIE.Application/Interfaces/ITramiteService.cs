using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ITramiteService
    {
        IEnumerable<Tramite> ListarTodos();
        Tramite? BuscarPorId(int id);
        void Crear(Tramite tramite);
        void Actualizar(Tramite tramite);
        void Eliminar(int id);
    }
}