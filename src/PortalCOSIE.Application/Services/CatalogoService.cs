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
        private readonly IGenericRepo<SesionCOSIE> _sesionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CatalogoService(
            IGenericRepo<EstadoTramite> estadoTramiteRepository,
            IGenericRepo<TipoTramite> tipoTramiteRepository,
            IGenericRepo<EstadoDocumento> estadoDocumentoRepository,
            IGenericRepo<SesionCOSIE> sesionRepository,
            IUnitOfWork unitOfWork)
        {
            _estadoTramiteRepository = estadoTramiteRepository;
            _tipoTramiteRepository = tipoTramiteRepository;
            _estadoDocumentoRepository = estadoDocumentoRepository;
            _sesionRepository = sesionRepository;
            _unitOfWork = unitOfWork;
        }

        //EstadoTramite CRUD
        public async Task<IEnumerable<EstadoTramite>> ListarEstadosTramite()
        {
            return await _estadoTramiteRepository.GetAllAsync();
        }
        public async Task<IEnumerable<EstadoTramite>> ListarTodoEstadoTramite()
        {
            return await _estadoTramiteRepository.GetAllAsync(true);
        }
        public async Task CrearEstadoTramite(string nombre)
        {
            await _unitOfWork.GenericRepo<EstadoTramite>()
                .AddAsync(new EstadoTramite(nombre));
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task EditarEstadoTramite(int id, string nombre)
        {
            var estadoTramite = await _estadoTramiteRepository.GetByIdAsync(id, true);
            estadoTramite.ActualizarNombre(nombre);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task ToggleEstadoTramite(int id)
        {
            var estado = await _estadoTramiteRepository.GetByIdAsync(id, true);
            if (estado.IsDeleted)
                estado.Restore();
            else
                estado.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
        }

        //EstadoDocumento CRUD
        public async Task<IEnumerable<EstadoDocumento>> ListarEstadosDocumento()
        {
            return await _estadoDocumentoRepository.GetAllAsync();
        }
        public async Task<IEnumerable<EstadoDocumento>> ListarTodoEstadosDocumento()
        {
            return await _estadoDocumentoRepository.GetAllAsync(true);
        }
        public async Task CrearEstadoDocumento(string nombre)
        {
            await _unitOfWork.GenericRepo<EstadoDocumento>()
                .AddAsync(new EstadoDocumento(nombre));
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task EditarEstadoDocumento(int id, string nombre)
        {
            var estadoDocumento = await _estadoDocumentoRepository.GetByIdAsync(id, true);
            estadoDocumento.ActualizarNombre(nombre);

            await _unitOfWork.SaveChangesAsync();
        }
        public async Task ToggleEstadoDocumento(int id)
        {
            var estado = await _estadoDocumentoRepository.GetByIdAsync(id, true);
            if (estado.IsDeleted)
                estado.Restore();
            else
                estado.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
        }

        //TipoTramite CRUD
        public async Task<IEnumerable<TipoTramite>> ListarTipoTramite()
        {
            return await _tipoTramiteRepository.GetAllAsync();
        }
        public async Task<IEnumerable<TipoTramite>> ListarTodoTipoTramite()
        {
            return await _tipoTramiteRepository.GetAllAsync(true);
        }
        public async Task CrearTipoTramite(string nombre)
        {
            await _unitOfWork.GenericRepo<TipoTramite>()
                .AddAsync(new TipoTramite(nombre));
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task EditarTipoTramite(int id, string nombre)
        {
            var tipoTramite = await _tipoTramiteRepository.GetByIdAsync(id, true);
            tipoTramite.ActualizarNombre(nombre);

            await _unitOfWork.SaveChangesAsync();
        }
        public async Task ToggleTipoTramite(int id)
        {
            var tipo = await _tipoTramiteRepository.GetByIdAsync(id, true);
            if (tipo.IsDeleted)
                tipo.Restore();
            else
                tipo.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PeriodoConfig> ListarPeriodoConfig()
        {
            if (await _unitOfWork.GenericRepo<PeriodoConfig>().GetFirstOrDefaultAsync(x => x.Id == 1) == null)
            {
                var nuevo = new PeriodoConfig(2010, 1, DateTime.Now.Year + 1, 1);
                await _unitOfWork.GenericRepo<PeriodoConfig>().AddAsync(nuevo);
                await _unitOfWork.SaveChangesAsync();
                return nuevo;
            }
            return await _unitOfWork.GenericRepo<PeriodoConfig>().GetByIdAsync(1);
        }
        public async Task<IEnumerable<string>> ListarPeriodos()
        {
            var periodoConfig = await _unitOfWork.GenericRepo<PeriodoConfig>().GetByIdAsync(1);

            if (periodoConfig == null)
                throw new Exception("No existe configuración de periodos.");

            var periodos = new List<string>();

            int anioInicio = periodoConfig.AnioInicio;
            int anioFin = periodoConfig.AnioFin;

            for (int anio = anioInicio; anio <= anioFin; anio++)
            {
                int periodoInicio = (anio == anioInicio) ? periodoConfig.PeriodoInicio : 1;
                int periodoFin = (anio == anioFin) ? periodoConfig.PeriodoFin : 2;

                for (int p = periodoInicio; p <= periodoFin; p++)
                {
                    periodos.Add($"{anio}/{p}");
                }
            }
            return periodos;
        }
        public async Task EditarPeriodoConfig(int anioInicio, int periodoInicio, int anioFin, int periodoFin)
        {
            var periodo = await _unitOfWork.GenericRepo<PeriodoConfig>().GetByIdAsync(1);
            if (periodo == null)
                throw new Exception("Configuracion de periodo no encontrada");
            periodo.SetAnioInicio(anioInicio);
            periodo.SetPeriodoInicio(periodoInicio);
            periodo.SetAnioFin(anioFin);
            periodo.SetPeriodoFin(periodoFin);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<SesionCOSIE>> ListarSesiones()
        {
            var sesiones = await _sesionRepository.GetAllAsync(true);

            return sesiones;
        }
    }
}