using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Dashboard.DTO;

namespace PortalCOSIE.Application.Features.Dashboard.Queries.ObtenerEstadoTramitesCTCE
{
    public sealed record ObtenerEstadoTramitesCTCEQuery(string Periodo) : IRequest<ChartDTO>;
}
