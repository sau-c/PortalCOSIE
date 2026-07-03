using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Usuarios.DTO;

namespace PortalCOSIE.Application.Features.Security.Commands.RestablecerContrasena;

public sealed record RestablecerContrasenaCommand(RestablecerDTO Dto) : IRequest<Result<string>>;