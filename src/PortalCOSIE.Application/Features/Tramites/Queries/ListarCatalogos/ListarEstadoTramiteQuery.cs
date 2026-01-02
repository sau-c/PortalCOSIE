using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Application.Features.Tramites.Queries.ListarEstadosTramite
{
    public sealed record ListarEstadoTramiteQuery : IRequest<IEnumerable<EstadoTramite>>;
}
