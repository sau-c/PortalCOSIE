using PortalCOSIE.Application.Abstractions;

namespace PortalCOSIE.Application.Features.Usuarios.Commands.ToggleAlumnoRol
{
    public sealed record ToggleAlumnoRolCommand(
        string UserId,
        string Rol,
        Stream? CertificadoCer) : IRequest<Result<string>>;
}