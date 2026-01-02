using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Dashboard.DTO;

namespace PortalCOSIE.Application.Features.Dashboard.Queries.ObtenerMasReprobadasCTCE
{
    public sealed record ObtenerMasReprobadasQuery(
        int? carreraId,
        string periodo
        ) : IRequest<ChartDTO>;
}
