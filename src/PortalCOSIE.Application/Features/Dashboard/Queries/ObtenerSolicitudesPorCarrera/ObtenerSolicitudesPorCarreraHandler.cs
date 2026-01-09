using PortalCOSIE.Application.Features.Dashboard.DTO;
using PortalCOSIE.Application.Services.Query;

namespace PortalCOSIE.Application.Features.Dashboard.Queries.ObtenerSolicitudesPorCarrera
{
    public class ObtenerSolicitudesPorCarreraHandler : IRequestHandler<ObtenerSolicitudesPorCarreraQuery, ChartDTO>
    {
        private readonly IDashboardQueryService _queryService;
        public ObtenerSolicitudesPorCarreraHandler(IDashboardQueryService queryService)
        {
            _queryService = queryService;
        }
        public async Task<ChartDTO> Handle(ObtenerSolicitudesPorCarreraQuery query)
        {
            return await _queryService.ObtenerSolicitudesPorCarrera(query.Periodo);
        }
    }
}
