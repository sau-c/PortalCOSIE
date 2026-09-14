namespace PortalCOSIE.Application.Features.Usuarios.Commands.ToggleAlumnoRol;

public sealed record ToggleAlumnoRolCommand(string UserId, string Rol) : IRequest<Result<string>>;