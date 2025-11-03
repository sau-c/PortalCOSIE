using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application
{
    public class TramiteService : ITramiteService
    {
        private readonly IBaseRepository<Tramite> _tramiteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TramiteService(
            IBaseRepository<Tramite> tramiteRepository,
            IUnitOfWork unitOfWork)
        {
            _tramiteRepository = tramiteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Tramite>> ListarTodos()
        {
            return await _tramiteRepository.GetAllAsync();
        }
        public async Task<Tramite?> BuscarPorId(int id)
        {
            return await _tramiteRepository.GetByIdAsync(id);
        }
        public async Task<Tramite> Crear(Tramite tramite)
        {
            return await _tramiteRepository.AddAsync(tramite);
        }
        public async Task Actualizar(Tramite tramite)
        {
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task Eliminar(int id)
        {
            var tramite = await _tramiteRepository.GetByIdAsync(id);
            tramite.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
        }
    }
}