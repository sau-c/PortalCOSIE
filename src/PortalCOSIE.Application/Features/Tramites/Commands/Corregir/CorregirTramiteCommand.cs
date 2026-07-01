using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Tramites.DTO;

namespace PortalCOSIE.Application.Features.Tramites.Commands.Corregir
{
    public sealed record CorregirTramiteCommand(
        string IdentityUserId,
        int TramiteId,
        DocumentoFirmadoDTO? CartaExposicionMotivos,
        DocumentoFirmadoDTO? Identificacion,
        DocumentoFirmadoDTO? BoletaGlobal,
        DocumentoFirmadoDTO? Probatorios
        ) : IRequest<Result<string>>;
}