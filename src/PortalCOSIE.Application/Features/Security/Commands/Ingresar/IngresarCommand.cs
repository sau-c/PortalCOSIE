using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Usuarios.DTO;

namespace PortalCOSIE.Application.Features.Security.Commands.Ingresar;

public sealed record IngresarCommand(IngresarDTO Dto) : IRequest<Result<string>>;