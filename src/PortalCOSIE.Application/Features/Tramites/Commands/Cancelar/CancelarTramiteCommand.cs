using PortalCOSIE.Application.Abstractions;

namespace PortalCOSIE.Application.Features.Tramites.Commands.Cancelar
{
    public sealed record CancelarTramiteCommand(
        string IdentityUserId,
        int tramiteId,
        string observaciones
        ) : IRequest<Result<string>>;
}
