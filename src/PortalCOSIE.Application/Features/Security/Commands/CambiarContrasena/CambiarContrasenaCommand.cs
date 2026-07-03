using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Usuarios.DTO;

namespace PortalCOSIE.Application.Features.Security.Commands.CambiarContrasena;

public sealed record CambiarContrasenaCommand(CambiarContrasenaDTO Dto) : IRequest<Result<string>>;