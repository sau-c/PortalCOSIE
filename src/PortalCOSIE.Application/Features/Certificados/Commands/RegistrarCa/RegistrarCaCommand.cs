namespace PortalCOSIE.Application.Features.Certificados.Commands.RegistrarCa;

public sealed record RegistrarCaCommand(Stream CerStream) : IRequest<Result<string>>;