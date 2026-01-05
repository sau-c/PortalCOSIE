using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.Tramites.CTCE;

namespace PortalCOSIE.Application.Features.Tramites.Queries.ObtenerTramiteCTCEPorId
{
    public sealed record ObtenerTramiteCTCEPorIdQuery(
        string IdentityUserId,
        string Rol,
        int TramiteId
        ) : IRequest<TramiteCTCE>;
}
