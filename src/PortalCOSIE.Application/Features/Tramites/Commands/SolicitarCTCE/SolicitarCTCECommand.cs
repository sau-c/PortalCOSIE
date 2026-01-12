using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Tramites.DTO;

namespace PortalCOSIE.Application.Features.Tramites.Commands.SolicitarCTCE
{
    public sealed record SolicitarCTCECommand(
        string IdentityUserId,
        bool TieneDictamenesAnteriores,
        string Peticion,
        List<UnidadReprobadaDTO> UnidadesReprobadas,
        ArchivoDTO CartaExposicionMotivos,
        ArchivoDTO Identificacion,
        ArchivoDTO BoletaGlobal,
        ArchivoDTO Probatorios,
        Stream LlaveKey,
        Stream CertificadoCer,
        string PasswordKey
        ) : IRequest<Result<string>>;
}
