using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application
{
    public class TramiteService : ITramiteService
    {
        private readonly IGenericRepo<Tramite> _tramiteRepository;
        private readonly IGenericRepo<EstadoTramite> _estadoTramiteRepository;
        private readonly IGenericRepo<TipoTramite> _tipoTramiteRepository;
        private readonly IGenericRepo<EstadoDocumento> _estadoDocumentoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TramiteService(
            IGenericRepo<Tramite> tramiteRepository,
            IGenericRepo<EstadoTramite> estadoTramiteRepository,
            IGenericRepo<TipoTramite> tipoTramiteRepository,
            IGenericRepo<EstadoDocumento> estadoDocumentoRepository,
            IUnitOfWork unitOfWork)
        {
            _tramiteRepository = tramiteRepository;
            _estadoTramiteRepository = estadoTramiteRepository;
            _tipoTramiteRepository = tipoTramiteRepository;
            _estadoDocumentoRepository = estadoDocumentoRepository;
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
            await _tramiteRepository.UpdateAsync(tramite);
        }
        public async Task Eliminar(int id)
        {
            var tramite = await _tramiteRepository.GetByIdAsync(id);
            await _tramiteRepository.DeleteAsync(tramite);
        }
    }
}