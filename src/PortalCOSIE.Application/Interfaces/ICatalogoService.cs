using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ICatalogoService<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> ListarActivosAsync();
        Task<IEnumerable<T>> ListarAsync();
        Task ToggleAsync(int id);
    }
}
