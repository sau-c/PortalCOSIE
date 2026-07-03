using PortalCOSIE.Application.Abstractions;

namespace PortalCOSIE.Application.Features.Security.Commands.ConfirmarCorreo;

public sealed record ConfirmarCorreoCommand(string Correo, string Token) : IRequest<Result<string>>;