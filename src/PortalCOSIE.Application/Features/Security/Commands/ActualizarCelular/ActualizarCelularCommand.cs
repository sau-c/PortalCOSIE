using PortalCOSIE.Application.Abstractions;

namespace PortalCOSIE.Application.Features.Security.Commands.ActualizarCelular;

public sealed record ActualizarCelularCommand(string UserId, string Celular) : IRequest<Result<string>>;