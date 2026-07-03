using PortalCOSIE.Application.Abstractions;

namespace PortalCOSIE.Application.Features.Security.Commands.ToggleRol;

public sealed record ToggleRolCommand(string UserId, string Rol) : IRequest<Result<string>>;