using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Security.Commands.ConfirmarCorreo;

public class ConfirmarCorreoHandler(ISecurityService security)
    : IRequestHandler<ConfirmarCorreoCommand, Result<string>>
{
    public Task<Result<string>> Handle(ConfirmarCorreoCommand command)
        => security.ConfirmarCorreo(command.Correo, command.Token);
}