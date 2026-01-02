using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Application.Features.Tramites.Queries.ListarTramites
{
    public sealed record ListarTramitesQuery(
        string IdentityUserId,
        string rol
        ) : IRequest<IEnumerable<Tramite>>;
}
