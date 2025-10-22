using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application
{
    public class CatalogoService : ICatalogoService
    {
        private readonly IGenericRepo<EstadoTramite> _estadoTramiteRepository;
        private readonly IGenericRepo<TipoTramite> _tipoTramiteRepository;
        private readonly IGenericRepo<EstadoDocumento> _estadoDocumentoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CatalogoService(
            IGenericRepo<EstadoTramite> estadoTramiteRepository,
            IGenericRepo<TipoTramite> tipoTramiteRepository,
            IGenericRepo<EstadoDocumento> estadoDocumentoRepository,
            IUnitOfWork unitOfWork)
        {
            _estadoTramiteRepository = estadoTramiteRepository;
            _tipoTramiteRepository = tipoTramiteRepository;
            _estadoDocumentoRepository = estadoDocumentoRepository;
            _unitOfWork = unitOfWork;
        }

        //EstadoTramite CRUD
        public async Task<IEnumerable<EstadoTramite>> ListarEstados()
        {
            return await _estadoTramiteRepository.GetAllAsync();
        }
        public async Task CrearEstado(EstadoTramite estadoTramite)
        {
            await _unitOfWork.GenericRepo<EstadoTramite>().AddAsync(estadoTramite);
            await _unitOfWork.CompleteAsync();
        }
        public async Task EditarEstado(EstadoTramite estadoTramite)
        {
            await _estadoTramiteRepository.UpdateAsync(estadoTramite);
        }
        public async Task EliminarEstado(int id)
        {
            var estado = await _estadoTramiteRepository.GetByIdAsync(id);
            await _unitOfWork.GenericRepo<EstadoTramite>().DeleteAsync(estado);
            await _unitOfWork.CompleteAsync();
        }

        //EstadoDocumento CRUD
        public async Task<IEnumerable<EstadoDocumento>> ListarEstadosDocumento()
        {
            return await _estadoDocumentoRepository.GetAllAsync();
        }
        public async Task EliminarEstadoDocumento(int id)
        {
            var estado = await _estadoDocumentoRepository.GetByIdAsync(id);
            await _unitOfWork.GenericRepo<EstadoDocumento>().DeleteAsync(estado);
            await _unitOfWork.CompleteAsync();
        }
        public async Task EditarEstadoDocumento(EstadoDocumento estadoDocumento)
        {
            await _estadoDocumentoRepository.UpdateAsync(estadoDocumento);
        }
        public async Task CrearEstadoDocumento(EstadoDocumento estadoDocumento)
        {
            await _unitOfWork.GenericRepo<EstadoDocumento>().AddAsync(estadoDocumento);
            await _unitOfWork.CompleteAsync();
        }

        //TipoTramite CRUD
        public async Task<IEnumerable<TipoTramite>> ListarTipoTramite()
        {
            return await _tipoTramiteRepository.GetAllAsync();
        }
        public async Task EliminarTipoTramite(int id)
        {
            var estado = await _tipoTramiteRepository.GetByIdAsync(id);
            await _unitOfWork.GenericRepo<TipoTramite>().DeleteAsync(estado);
            await _unitOfWork.CompleteAsync();
        }
        public async Task EditarTipoTramite(TipoTramite tipoTramite)
        {
            await _tipoTramiteRepository.UpdateAsync(tipoTramite);
        }
        public async Task CrearTipoTramite(TipoTramite tipoTramite)
        {
            await _unitOfWork.GenericRepo<TipoTramite>().AddAsync(tipoTramite);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<PeriodoConfig> ListarConfiguracionPeriodos()
        {
            if (await _unitOfWork.GenericRepo<PeriodoConfig>().GetFirstOrDefaultAsync(x => x.Id == 1) == null)
            {
                var nuevo = new PeriodoConfig
                {
                    PeriodoInicio = "20101",
                    PeriodoFin = DateTime.Now.Year.ToString() + "2",
                    PeriodosPorAnio = 2
                };
                await _unitOfWork.GenericRepo<PeriodoConfig>().AddAsync(nuevo);
                await _unitOfWork.CompleteAsync();
                return nuevo;
            }
            return await _unitOfWork.GenericRepo<PeriodoConfig>().GetByIdAsync(1);
        }
    }
}