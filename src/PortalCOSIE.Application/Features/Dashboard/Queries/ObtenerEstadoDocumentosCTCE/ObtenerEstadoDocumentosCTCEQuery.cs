using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Dashboard.DTO;

namespace PortalCOSIE.Application.Features.Dashboard.Queries.ObtenerEstadoDocumentosCTCE
{
    public sealed record ObtenerEstadoDocumentosCTCEQuery(string periodo) : IRequest<ChartDTO>;
}
