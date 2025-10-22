using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepo<T> GenericRepo<T>() where T : BaseEntity;
        Task<int> CompleteAsync();
    }
}
