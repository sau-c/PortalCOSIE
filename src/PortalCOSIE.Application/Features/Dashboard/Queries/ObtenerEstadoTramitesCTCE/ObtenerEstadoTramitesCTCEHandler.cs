using PortalCOSIE.Application.Features.Dashboard.DTO;
using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Dashboard.Queries.ObtenerEstadoTramitesCTCE
{
    public class ObtenerEstadoTramitesCTCEHandler : IRequestHandler<ObtenerEstadoTramitesCTCEQuery, ChartDTO>
    {
        private readonly IDashboardQueryService _queryService;
        public ObtenerEstadoTramitesCTCEHandler(IDashboardQueryService queryService)
        {
            _queryService = queryService;
        }
        public async Task<ChartDTO> Handle(ObtenerEstadoTramitesCTCEQuery query)
        {
            return await _queryService.ObtenerEstadoTramitesCTCE(query.Periodo);
        }
    }
}
