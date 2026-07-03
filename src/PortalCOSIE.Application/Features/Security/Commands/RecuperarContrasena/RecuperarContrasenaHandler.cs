using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Security.Commands.RecuperarContrasena;

public class RecuperarContrasenaHandler(ISecurityService security)
    : IRequestHandler<RecuperarContrasenaCommand, Result<string>>
{
    public Task<Result<string>> Handle(RecuperarContrasenaCommand command)
        => security.RecuperarContrasena(command.Correo);
}