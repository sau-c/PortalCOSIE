using PortalCOSIE.Application.Abstractions;

namespace PortalCOSIE.Application.Features.Tramites.Commands.CancelarTramite
{
    public sealed record CancelarTramiteCommand(
        int tramiteId,
        string observaciones
        ) : IRequest<Result<string>>;
}
