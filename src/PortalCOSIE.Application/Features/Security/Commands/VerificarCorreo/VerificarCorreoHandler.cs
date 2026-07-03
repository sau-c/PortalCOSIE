using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Security.Commands.VerificarCorreo;

public class VerificarCorreoHandler(ISecurityService security)
    : IRequestHandler<VerificarCorreoCommand, Result<string>>
{
    public Task<Result<string>> Handle(VerificarCorreoCommand command)
        => security.VerificarCorreo(command.UserId, command.Correo);
}