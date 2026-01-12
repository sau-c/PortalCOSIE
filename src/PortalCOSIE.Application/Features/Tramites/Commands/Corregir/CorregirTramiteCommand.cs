using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Tramites.DTO;

namespace PortalCOSIE.Application.Features.Tramites.Commands.Corregir
{
    public sealed record CorregirTramiteCommand(
        string IdentityUserId,
        int TramiteId,
        ArchivoDTO CartaExposicionMotivos,
        ArchivoDTO Identificacion,
        ArchivoDTO BoletaGlobal,
        ArchivoDTO Probatorios,
        Stream LlaveKey,
        Stream CertificadoCer,
        string PasswordKey
        ) : IRequest<Result<string>>;
}
