using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Tramites.DTO;

namespace PortalCOSIE.Application.Features.Tramites.Commands.Concluir
{
    public sealed record ConcluirTramiteCommand(
        string IdentityUserId,
        int TramiteId,
        DocumentoFirmadoDTO Acuse
        ) : IRequest<Result<string>>;
}