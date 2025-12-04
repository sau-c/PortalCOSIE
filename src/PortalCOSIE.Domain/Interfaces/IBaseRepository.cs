using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Domain.Interfaces
{
    /// <summary>
    /// Define un repositorio generico para manejar entidades del tipo <typeparamref name="TEntity"/>.
    /// </summary>
    /// <remarks>Esta interfaz proporciona operaciones comunes para trabajar con entidades
    /// Está diseñada para abstraer el mecanismo subyacente de acceso a datos,
    /// permitiendo una implementación flexible.</remarks>
    /// <typeparam name="TEntity">El tipo de entidad gestionada por el repositorio. Debe ser un tipo de referencia.</typeparam>
    public interface IBaseRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        Task<TEntity> GetByIdAsync(TId id);
        Task<IEnumerable<TEntity>> GetAllAsync(bool filtrarActivos);
        Task<TEntity> AddAsync(TEntity entity);
        TEntity Delete(TEntity entity);
    }
}
