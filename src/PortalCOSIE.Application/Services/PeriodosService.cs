using PortalCOSIE.Application.DTO.Periodo;
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
        public async Task<PeriodoConfigDTO> BuscarPeriodoConfig()
        {
            var config = await _periodoRepo.GetByIdAsync(1);
            if (config == null)
                throw new ApplicationException("No se encontró configuración de periodos.");
            return new PeriodoConfigDTO()
            {
                AnioInicio = config.AnioInicio,
                PeriodoInicio = config.PeriodoInicio,
                AnioActual = config.AnioActual,
                PeriodoActual = config.PeriodoActual,
            };
        }
        public async Task<IEnumerable<string>> ListarPeriodos()
        {
            var config = await _periodoRepo.GetByIdAsync(1);
            if (config == null)
                throw new ApplicationException("No se encontró configuración de periodos.");
            var periodos = new List<string>();

            for (int anio = config.AnioInicio; anio <= config.AnioActual; anio++)
            {
                int periodoInicio = (anio == config.AnioInicio) ? config.PeriodoInicio : 1;
                int periodoActual = (anio == config.AnioActual) ? config.PeriodoActual : 2;

                for (int p = periodoInicio; p <= periodoActual; p++)
                    periodos.Add($"{anio}/{p}");
            }

            return periodos;
        }

        public async Task EditarPeriodoConfig(PeriodoConfigDTO dto)
        {
            var config = await _periodoRepo.GetByIdAsync(1);
            if (config == null)
                throw new ApplicationException("No se encontró configuración de periodos.");
            config.SetAnioInicio(dto.AnioInicio);
            config.SetPeriodoInicio(dto.PeriodoInicio);
            config.SetAnioActual(dto.AnioActual);
            config.SetPeriodoActual(dto.PeriodoActual);

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