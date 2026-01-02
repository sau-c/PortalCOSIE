using PortalCOSIE.Application.Features.Dashboard.DTO;
using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Dashboard.Queries.ObtenerRolesAlumnos
{
    public class ObtenerRolesAlumnosHandler : IRequestHandler<ObtenerRolesAlumnosQuery, ChartDTO>
    {
        private readonly IDashboardQueryService _queryService;
        public ObtenerRolesAlumnosHandler(IDashboardQueryService queryService)
        {
            _queryService = queryService;
        }
        public async Task<ChartDTO> Handle(ObtenerRolesAlumnosQuery query)
        {
            return await _queryService.ObtenerRolesAlumnos();
        }
    }
}
