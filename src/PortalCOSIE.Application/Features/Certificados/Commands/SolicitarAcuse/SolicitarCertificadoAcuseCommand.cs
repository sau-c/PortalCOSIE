namespace PortalCOSIE.Application.Features.Certificados.Commands.SolicitarAcuse;

public sealed record SolicitarCertificadoAcuseCommand(string IdentityUserId, string CsrBase64)
    : IRequest<Result<string>>;