using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Tramites.DTO;

namespace PortalCOSIE.Application.Features.Tramites.Commands.SolicitarCTCE
{
    public sealed record SolicitarCTCECommand(
        SolicitudCtceDTO solicitud,
        string IdentityUserId
        ) : IRequest<Result<string>>;
}
