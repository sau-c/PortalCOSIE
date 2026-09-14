namespace PortalCOSIE.Application.Features.Certificados.Commands.SolicitarAlumno;

public sealed record SolicitarCertificadoAlumnoCommand(string IdentityUserId, string CsrBase64)
    : IRequest<Result<string>>;