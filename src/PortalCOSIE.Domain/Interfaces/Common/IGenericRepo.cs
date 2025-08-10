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
    public interface IGenericRepo<T> where T : class
    {
        /// <summary>
        /// Devuelve todas las entidades del tipo <typeparamref name="T"/>.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();
        /// <summary>
        /// Devuelve una entidad que cumple con el predicado especificado.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        //T? Get(Expression<Func<T, bool>> predicate);
        ///// <summary>
        ///// Devuelve una lista de entidades que cumplen con el predicado especificado.
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //IQueryable<T> GetList(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Devulve una entidad por su identificador.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
