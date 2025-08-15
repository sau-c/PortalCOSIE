namespace PortalCOSIE.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepo<T> GenericRepo<T>() where T : class;
        Task<int> CompleteAsync();
    }
}
