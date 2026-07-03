using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Security.Commands.CerrarSesion;

public class CerrarSesionHandler(ISecurityService security)
    : IRequestHandler<CerrarSesionCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CerrarSesionCommand command)
    {
        await security.CerrarSesionAsync();
        return Result<string>.Success("Sesión cerrada.");
    }
}