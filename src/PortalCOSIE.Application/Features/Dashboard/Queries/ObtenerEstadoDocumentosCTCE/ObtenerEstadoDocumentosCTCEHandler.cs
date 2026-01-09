using PortalCOSIE.Application.Features.Dashboard.DTO;
using PortalCOSIE.Application.Services.Query;

namespace PortalCOSIE.Application.Features.Dashboard.Queries.ObtenerEstadoDocumentosCTCE
{
    public class ObtenerEstadoDocumentosCTCEHandler : IRequestHandler<ObtenerEstadoDocumentosCTCEQuery, ChartDTO>
    {
        private readonly IDashboardQueryService _queryService;
        public ObtenerEstadoDocumentosCTCEHandler(IDashboardQueryService queryService)
        {
            _queryService = queryService;
        }
        public async Task<ChartDTO> Handle(ObtenerEstadoDocumentosCTCEQuery query)
        {
            return await _queryService.ObtenerEstadoDocumentosCTCE(query.periodo);
        }
    }
}
