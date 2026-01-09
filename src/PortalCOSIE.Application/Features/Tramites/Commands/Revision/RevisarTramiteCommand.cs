using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Tramites.DTO;

namespace PortalCOSIE.Application.Features.Tramites.Commands.Revision
{
    public sealed record RevisarTramiteCommand(
        int TramiteId,
        List<DocumentoDTO> Documentos,
        string Observaciones
        ) : IRequest<Result<string>>;
}
