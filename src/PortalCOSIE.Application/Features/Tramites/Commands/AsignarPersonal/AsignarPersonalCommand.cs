using PortalCOSIE.Application.Abstractions;

namespace PortalCOSIE.Application.Features.Tramites.Commands.AsignarPersonal
{
    public sealed record AsignarPersonalCommand(
        string IdentityUserId,
        int TramiteId
        ) : IRequest<Result<string>>;
}
