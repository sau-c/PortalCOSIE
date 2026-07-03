using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Usuarios.DTO;

namespace PortalCOSIE.Application.Features.Security.Commands.CrearCuenta;

public sealed record CrearCuentaCommand(CrearCuentaDTO Dto) : IRequest<Result<string>>;