using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Security.Commands.Ingresar;

public class IngresarHandler(ISecurityService security)
    : IRequestHandler<IngresarCommand, Result<string>>
{
    public Task<Result<string>> Handle(IngresarCommand command)
        => security.IngresarUsuarioAsync(command.Dto);
}