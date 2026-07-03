using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Security.Commands.CambiarContrasena;

public class CambiarContrasenaHandler(ISecurityService security)
    : IRequestHandler<CambiarContrasenaCommand, Result<string>>
{
    public Task<Result<string>> Handle(CambiarContrasenaCommand command)
        => security.CambiarContrasena(command.Dto);
}