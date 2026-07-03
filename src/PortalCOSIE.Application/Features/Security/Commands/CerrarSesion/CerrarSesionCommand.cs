using PortalCOSIE.Application.Abstractions;

namespace PortalCOSIE.Application.Features.Security.Commands.CerrarSesion;

public sealed record CerrarSesionCommand : IRequest<Result<string>>;