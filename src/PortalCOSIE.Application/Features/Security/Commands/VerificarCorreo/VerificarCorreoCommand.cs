using PortalCOSIE.Application.Abstractions;

namespace PortalCOSIE.Application.Features.Security.Commands.VerificarCorreo;

public sealed record VerificarCorreoCommand(string UserId, string Correo) : IRequest<Result<string>>;