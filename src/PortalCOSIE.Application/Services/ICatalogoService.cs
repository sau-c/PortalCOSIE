using PortalCOSIE.Domain.SharedKernel;

namespace PortalCOSIE.Application.Services
{
    public interface ICatalogoService<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        Task<IEnumerable<TEntity>> ListarActivosAsync();
        Task<IEnumerable<TEntity>> ListarAsync();
        Task ToggleAsync(TId id);
    }
}
