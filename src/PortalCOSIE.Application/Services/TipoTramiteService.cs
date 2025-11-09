using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Services
{
    public class TipoTramiteService : ITipoTramiteService
    {
        private readonly IBaseRepository<TipoTramite> _tipoTramiteRepo;
        private readonly IUnitOfWork _unitOfWork;

        public TipoTramiteService(
            IBaseRepository<TipoTramite> tipoTramiteRepo,
            IUnitOfWork unitOfWork)
        {
            _tipoTramiteRepo = tipoTramiteRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TipoTramite>> ListarTipoTramiteActivos()
        {
            return await _tipoTramiteRepo.GetAllAsync(true);
        }
        public async Task<IEnumerable<TipoTramite>> ListarTipoTramite()
        {
            return await _tipoTramiteRepo.GetAllAsync(false);
        }
        public async Task CrearTipoTramite(string nombre)
        {
            await _tipoTramiteRepo.AddAsync(new TipoTramite(nombre));
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task EditarTipoTramite(int id, string nombre)
        {
            var tipoTramite = await _tipoTramiteRepo.GetByIdAsync(id);
            tipoTramite.ActualizarNombre(nombre);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task ToggleTipoTramite(int id)
        {
            var tipo = await _tipoTramiteRepo.GetByIdAsync(id);
            if (tipo.IsDeleted)
                tipo.Restore();
            else
                tipo.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
