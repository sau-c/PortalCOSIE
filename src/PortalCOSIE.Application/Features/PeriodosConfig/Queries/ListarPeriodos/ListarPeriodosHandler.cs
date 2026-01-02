using PortalCOSIE.Domain.Entities.PeriodosConfig;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.PeriodosConfig.Queries.ListarPeriodos
{
    public class ListarPeriodosHandler : IRequestHandler<ListarPeriodosQuery, IEnumerable<string>>
    {
        private readonly IBaseRepository<PeriodoConfig, int> _periodoRepo;
        public ListarPeriodosHandler(IBaseRepository<PeriodoConfig, int> periodoRepo)
        {
            _periodoRepo = periodoRepo;
        }
        public async Task<IEnumerable<string>> Handle(ListarPeriodosQuery query)
        {
            var config = await _periodoRepo.GetByIdAsync(1);
            if (config == null)
                throw new ApplicationException("No se encontró configuración de periodos.");
            var periodos = new List<string>();

            for (int anio = config.AnioInicio; anio <= config.AnioActual; anio++)
            {
                int periodoInicio = anio == config.AnioInicio ? config.PeriodoInicio : 1;
                int periodoActual = anio == config.AnioActual ? config.PeriodoActual : 2;

                for (int p = periodoInicio; p <= periodoActual; p++)
                    periodos.Add($"{anio}/{p}");
            }

            return periodos.OrderDescending();
        }
    }
}
