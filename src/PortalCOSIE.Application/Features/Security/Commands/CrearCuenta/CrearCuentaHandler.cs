using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Security.Commands.CrearCuenta;

public class CrearCuentaHandler(ISecurityService security)
    : IRequestHandler<CrearCuentaCommand, Result<string>>
{
    public Task<Result<string>> Handle(CrearCuentaCommand command)
        => security.CrearUsuario(command.Dto);
}