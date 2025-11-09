using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Services
{
    public class EstadoTramiteService : IEstadoTramiteService
    {
        private readonly IBaseRepository<EstadoTramite> _estadoTramiteRepo;
        private readonly IUnitOfWork _unitOfWork;

        public EstadoTramiteService(
            IBaseRepository<EstadoTramite> estadoTramiteRepo,
            IUnitOfWork unitOfWork)
        {
            _estadoTramiteRepo = estadoTramiteRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EstadoTramite>> ListarEstadosTramiteActivos()
        {
            return await _estadoTramiteRepo.GetAllAsync(true);
        }
        public async Task<IEnumerable<EstadoTramite>> ListarEstadoTramite()
        {
            return await _estadoTramiteRepo.GetAllAsync(false);
        }
        public async Task CrearEstadoTramite(string nombre)
        {
            await _estadoTramiteRepo.AddAsync(new EstadoTramite(nombre));
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task EditarEstadoTramite(int id, string nombre)
        {
            var estadoTramite = await _estadoTramiteRepo.GetByIdAsync(id);
            estadoTramite.ActualizarNombre(nombre);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task ToggleEstadoTramite(int id)
        {
            var estado = await _estadoTramiteRepo.GetByIdAsync(id);
            if (estado.IsDeleted)
                estado.Restore();
            else
                estado.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
