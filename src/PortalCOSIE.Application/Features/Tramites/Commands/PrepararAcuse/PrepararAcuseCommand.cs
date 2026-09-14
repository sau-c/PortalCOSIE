using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Tramites.DTO;

namespace PortalCOSIE.Application.Features.Tramites.Commands.PrepararAcuse;

public sealed record PrepararAcuseCommand(
    string IdentityUserId,
    int TramiteId,
    DocumentoFirmadoDTO Acuse,
    string BaseUrl
) : IRequest<Result<PrepararAcuseResultDTO>>;