using PortalCOSIE.Domain.Entities;
using System.Linq.Expressions;

namespace PortalCOSIE.Domain.Interfaces
{
    /// <summary>
    /// Define un repositorio generico para manejar entidades del tipo <typeparamref name="T"/>.
    /// </summary>
    /// <remarks>Esta interfaz proporciona operaciones CRUD comunes para trabajar con entidades, incluyendo
    /// recuperación, adición, actualización y eliminación. Está diseñada para abstraer el mecanismo subyacente de acceso a datos,
    /// permitiendo una implementación flexible.</remarks>
    /// <typeparam name="T">El tipo de entidad gestionada por el repositorio. Debe ser un tipo de referencia.</typeparam>
    public interface IGenericRepo<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id, bool includeDeleted = false);
        Task<IEnumerable<T>> GetAllAsync(bool includeDeleted = false);
        Task<T> AddAsync(T entity);
        T Delete(T entity);

        // Para eager loading
        IQueryable<T> Query();
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T?>> GetListAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
    }
}
