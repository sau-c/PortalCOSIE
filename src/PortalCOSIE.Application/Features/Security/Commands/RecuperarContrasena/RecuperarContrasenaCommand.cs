using PortalCOSIE.Application.Abstractions;

namespace PortalCOSIE.Application.Features.Security.Commands.RecuperarContrasena;

public sealed record RecuperarContrasenaCommand(string Correo) : IRequest<Result<string>>;