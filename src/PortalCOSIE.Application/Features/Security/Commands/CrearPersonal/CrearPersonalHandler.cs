using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Security.Commands.CrearPersonal;

public class CrearPersonalHandler(ISecurityService security)
    : IRequestHandler<CrearPersonalCommand, Result<string>>
{
    public Task<Result<string>> Handle(CrearPersonalCommand command)
        => security.CrearPersonal(command.Dto);
}