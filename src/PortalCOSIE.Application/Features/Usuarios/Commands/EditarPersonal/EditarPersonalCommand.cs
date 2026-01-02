using PortalCOSIE.Application.Abstractions;

namespace PortalCOSIE.Application.Features.Usuarios.Commands.EditarPersonal
{
    public sealed record EditarPersonalCommand(
        string IdentityUserId,
        string Nombre,
        string ApellidoPaterno,
        string ApellidoMaterno
        ) : IRequest<Result<string>>;
}
