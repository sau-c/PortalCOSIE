using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Dashboard.DTO;

namespace PortalCOSIE.Application.Features.Dashboard.Queries.ObtenerSolicitudesPorCarrera
{
    public sealed record ObtenerSolicitudesPorCarreraQuery(string Periodo) : IRequest<ChartDTO>;
}
