using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Security.Commands.ActualizarCorreo;

public class ActualizarCorreoHandler(ISecurityService security)
    : IRequestHandler<ActualizarCorreoCommand, Result<string>>
{
    public Task<Result<string>> Handle(ActualizarCorreoCommand command)
        => security.ActualizarCorreo(command.Id, command.Correo, command.Token);
}