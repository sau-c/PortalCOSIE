using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Security.Commands.ActualizarCelular;

public class ActualizarCelularHandler(ISecurityService security)
    : IRequestHandler<ActualizarCelularCommand, Result<string>>
{
    public Task<Result<string>> Handle(ActualizarCelularCommand command)
        => security.ActualizarCelular(command.UserId, command.Celular);
}