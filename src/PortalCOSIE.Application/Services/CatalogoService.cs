using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Services
{
    public class CatalogoService<T> : ICatalogoService<T> where T : BaseEntity
    {
        private readonly IBaseRepository<T> _catalogoRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CatalogoService(
            IBaseRepository<T> catalogoRepo,
            IUnitOfWork unitOfWork)
        {
            _catalogoRepo = catalogoRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<T>> ListarActivosAsync()
        {
            return await _catalogoRepo.GetAllAsync(true);
        }

        public async Task<IEnumerable<T>> ListarAsync()
        {
            return await _catalogoRepo.GetAllAsync(false);
        }

        public async Task ToggleAsync(int id)
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
