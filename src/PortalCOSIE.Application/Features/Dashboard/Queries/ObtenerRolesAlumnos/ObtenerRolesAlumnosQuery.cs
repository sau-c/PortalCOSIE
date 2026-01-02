using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Dashboard.DTO;

namespace PortalCOSIE.Application.Features.Dashboard.Queries.ObtenerRolesAlumnos
{
    public sealed record ObtenerRolesAlumnosQuery() : IRequest<ChartDTO>;
}
