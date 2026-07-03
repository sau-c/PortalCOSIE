using PortalCOSIE.Application.Abstractions;

namespace PortalCOSIE.Application.Features.Security.Commands.ActualizarCorreo;

public sealed record ActualizarCorreoCommand(string Id, string Correo, string Token) : IRequest<Result<string>>;