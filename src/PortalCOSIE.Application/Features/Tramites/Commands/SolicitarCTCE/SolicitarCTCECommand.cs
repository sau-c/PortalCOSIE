using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Tramites.DTO;

namespace PortalCOSIE.Application.Features.Tramites.Commands.SolicitarCTCE
{
    public sealed record SolicitarCTCECommand(
        string IdentityUserId,
        bool TieneDictamenesAnteriores,
        string Peticion,
        List<UnidadReprobadaDTO> UnidadesReprobadas,
        DocumentoFirmadoDTO CartaExposicionMotivos,
        DocumentoFirmadoDTO Identificacion,
        DocumentoFirmadoDTO BoletaGlobal,
        DocumentoFirmadoDTO Probatorios
        ) : IRequest<Result<string>>;
}