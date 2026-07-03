using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Security.Commands.ToggleRol;

public class ToggleRolHandler(ISecurityService security)
    : IRequestHandler<ToggleRolCommand, Result<string>>
{
    public Task<Result<string>> Handle(ToggleRolCommand command)
        => security.ToggleRol(command.UserId, command.Rol);
}