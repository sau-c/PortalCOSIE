using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<T> BaseRepo<T>() where T : BaseEntity;
        //Task<int> CommitAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
