using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Services
{
    public class EstadoDocumentoService : IEstadoDocumentoService
    {
        private readonly IBaseRepository<EstadoDocumento> _estadoDocumentoRepo;
        private readonly IUnitOfWork _unitOfWork;

        public EstadoDocumentoService(
            IBaseRepository<EstadoDocumento> estadoTramiteRepo,
            IUnitOfWork unitOfWork)
        {
            _estadoDocumentoRepo = estadoTramiteRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EstadoDocumento>> ListarEstadosDocumentoActivos()
        {
            return await _estadoDocumentoRepo.GetAllAsync(true);
        }
        public async Task<IEnumerable<EstadoDocumento>> ListarEstadosDocumento()
        {
            return await _estadoDocumentoRepo.GetAllAsync(false);
        }
        public async Task CrearEstadoDocumento(string nombre)
        {
            await _estadoDocumentoRepo.AddAsync(new EstadoDocumento(nombre));
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task EditarEstadoDocumento(int id, string nombre)
        {
            var estadoDocumento = await _estadoDocumentoRepo.GetByIdAsync(id);
            estadoDocumento.ActualizarNombre(nombre);

            await _unitOfWork.SaveChangesAsync();
        }
        public async Task ToggleEstadoDocumento(int id)
        {
            var estado = await _estadoDocumentoRepo.GetByIdAsync(id);
            if (estado.IsDeleted)
                estado.Restore();
            else
                estado.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
