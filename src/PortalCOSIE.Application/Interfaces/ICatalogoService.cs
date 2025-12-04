using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ICatalogoService<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        Task<IEnumerable<TEntity>> ListarActivosAsync();
        Task<IEnumerable<TEntity>> ListarAsync();
        Task ToggleAsync(TId id);
    }
}
