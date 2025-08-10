
namespace PortalCOSIE.Application.Interfaces.Common
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> ListarTodos();
        T ObtenerPorId(int id);
        void Crear(T entity);
        void Actualizar(T entity);
        void Eliminar(int id);
    }
}
