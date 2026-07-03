using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Security.Commands.RestablecerContrasena;

public class RestablecerContrasenaHandler(ISecurityService security)
    : IRequestHandler<RestablecerContrasenaCommand, Result<string>>
{
    public Task<Result<string>> Handle(RestablecerContrasenaCommand command)
        => security.RestablecerContrasena(command.Dto);
}