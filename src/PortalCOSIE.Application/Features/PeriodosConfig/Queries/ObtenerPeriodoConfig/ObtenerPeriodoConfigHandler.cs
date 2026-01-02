using PortalCOSIE.Domain.Entities.PeriodosConfig;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.PeriodosConfig.Queries.ObtenerPeriodoConfig
{
    public class ObtenerPeriodoConfigHandler : IRequestHandler<ObtenerPeriodoConfigQuery, PeriodoConfig>
    {
        private readonly IBaseRepository<PeriodoConfig, int> _periodoRepo;
        public ObtenerPeriodoConfigHandler(IBaseRepository<PeriodoConfig, int> periodoRepo)
        {
            _periodoRepo = periodoRepo;
        }
        public async Task<PeriodoConfig> Handle(ObtenerPeriodoConfigQuery query)
        {
            var config = await _periodoRepo.GetByIdAsync(1);
            if (config == null)
                throw new ApplicationException("No se encontró configuración de periodos.");
            return config;
        }
    }
}
