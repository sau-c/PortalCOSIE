using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities.Calendario;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application
{
    public class PeriodosService : IPeriodosService
    {
        private readonly IBaseRepository<PeriodoConfig> _periodoRepo;
        private readonly ISesionRepository _sesionRepo;
        private readonly IUnitOfWork _unitOfWork;

        public PeriodosService(
            ISesionRepository sesionRepo,
            IBaseRepository<PeriodoConfig> periodoRepo,
            IUnitOfWork unitOfWork)
        {
            _sesionRepo = sesionRepo;
            _periodoRepo = periodoRepo;
            _unitOfWork = unitOfWork;
        }

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