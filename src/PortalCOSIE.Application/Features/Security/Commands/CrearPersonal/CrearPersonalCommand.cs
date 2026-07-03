using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Usuarios.DTO;

namespace PortalCOSIE.Application.Features.Security.Commands.CrearPersonal;

public sealed record CrearPersonalCommand(CrearPersonalDTO Dto) : IRequest<Result<string>>;