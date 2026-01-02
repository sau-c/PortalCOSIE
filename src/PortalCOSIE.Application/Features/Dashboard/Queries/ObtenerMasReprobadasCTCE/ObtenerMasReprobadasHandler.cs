using PortalCOSIE.Application.Features.Dashboard.DTO;
using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Dashboard.Queries.ObtenerMasReprobadasCTCE
{
    public class ObtenerMasReprobadasHandler : IRequestHandler<ObtenerMasReprobadasQuery, ChartDTO>
    {
        private readonly IDashboardQueryService _queryService;
        public ObtenerMasReprobadasHandler(IDashboardQueryService queryService)
        {
            _queryService = queryService;
        }
        public async Task<ChartDTO> Handle(ObtenerMasReprobadasQuery query)
        {
            return await _queryService.ObtenerUnidadesMasReprobadasPorCarrera(query.carreraId, query.periodo);
        }
    }
}
