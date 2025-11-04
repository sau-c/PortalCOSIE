using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities.Calendario;
using PortalCOSIE.Domain.Interfaces;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Application
{
    public class CatalogoService : ICatalogoService
    {
        private readonly IBaseRepository<EstadoTramite> _estadoTramiteRepo;
        private readonly IBaseRepository<TipoTramite> _tipoTramiteRepo;
        private readonly IBaseRepository<PeriodoConfig> _periodoRepo;
        private readonly IBaseRepository<EstadoDocumento> _estadoDocumentoRepo;
        private readonly ISesionRepository _sesionRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CatalogoService(
            IBaseRepository<EstadoTramite> estadoTramiteRepo,
            IBaseRepository<TipoTramite> tipoTramiteRepo,
            IBaseRepository<EstadoDocumento> estadoDocumentoRepo,
            ISesionRepository sesionRepo,
            IBaseRepository<PeriodoConfig> periodoRepo,
            IUnitOfWork unitOfWork)
        {
            _estadoTramiteRepo = estadoTramiteRepo;
            _tipoTramiteRepo = tipoTramiteRepo;
            _estadoDocumentoRepo = estadoDocumentoRepo;
            _sesionRepo = sesionRepo;
            _periodoRepo = periodoRepo;
            _unitOfWork = unitOfWork;
        }

        #region ESTADO_TRAMITE
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
        #endregion

        #region ESTADO_DOCUMENTO
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
        #endregion

        #region TIPO_TRAMITE
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
        #endregion

        #region PERIODO_CONFIG
        public async Task<PeriodoConfig> BuscarPeriodoConfig()
        {
            if (await _periodoRepo.GetByIdAsync(1) == null)
            {
                var nuevo = new PeriodoConfig(2010, 1, DateTime.Now.Year + 1, 1);
                await _periodoRepo.AddAsync(nuevo);
                await _unitOfWork.SaveChangesAsync();
                return nuevo;
            }
            return await _periodoRepo.GetByIdAsync(1);
        }
        public async Task<IEnumerable<string>> ListarPeriodos()
        {
            var periodoConfig = await _periodoRepo.GetByIdAsync(1);

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
            var periodo = await _periodoRepo.GetByIdAsync(1);
            if (periodo == null)
                throw new Exception("Configuracion de periodo no encontrada");
            periodo.SetAnioInicio(anioInicio);
            periodo.SetPeriodoInicio(periodoInicio);
            periodo.SetAnioFin(anioFin);
            periodo.SetPeriodoFin(periodoFin);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region SESION_COSIE
        public async Task<IEnumerable<SesionCOSIE>> ListarSesiones()
        {
            return await _sesionRepo.ListarSesiones(false);
        }
        public async Task<IEnumerable<SesionCOSIE>> ListarSesionesActivas()
        {
            return await _sesionRepo.ListarSesiones(true);
        }
        public async Task CrearSesion(string numeroSesion, DateTime fechaSesion, List<DateTime> fechasRecepcion)
        {
            var sesion = new SesionCOSIE(
                numeroSesion,
                fechaSesion
                );
            sesion.SetFechasRecepcion(fechasRecepcion);
            await _sesionRepo.AddAsync(sesion);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task EditarSesion(int id, string numeroSesion, DateTime fechaSesion, List<DateTime> fechasRecepcion)
        {
            var sesion = await _sesionRepo.ObtenerConFechasRecepcion(id);

            if (sesion == null)
                throw new Exception("No se encontro el registro");
            sesion.SetNumeroSesion(numeroSesion);
            sesion.SetFechaSesion(fechaSesion);
            sesion.SetFechasRecepcion(fechasRecepcion);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task ToggleSesion(int id)
        {
            var sesion = await _sesionRepo.GetByIdAsync(id);
            if (sesion.IsDeleted)
                sesion.Restore();
            else
                sesion.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion
    }
}