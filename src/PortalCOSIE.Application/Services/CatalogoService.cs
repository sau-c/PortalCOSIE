using PortalCOSIE.Domain.Interfaces;
using PortalCOSIE.Domain.SharedKernel;

namespace PortalCOSIE.Application.Services
{
    public class CatalogoService<TEntity, TId> : ICatalogoService<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        private readonly IBaseRepository<TEntity, TId> _catalogoRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CatalogoService(
            IBaseRepository<TEntity, TId> catalogoRepo,
            IUnitOfWork unitOfWork)
        {
            _catalogoRepo = catalogoRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TEntity>> ListarActivosAsync()
        {
            return await _catalogoRepo.GetAllAsync(true);
        }

        public async Task<IEnumerable<TEntity>> ListarAsync()
        {
            return await _catalogoRepo.GetAllAsync();
        }

        public async Task ToggleAsync(TId id)
        {
            var entidad = await _catalogoRepo.GetByIdAsync(id);
            if (entidad.IsDeleted)
                entidad.Restore();
            else
                entidad.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
