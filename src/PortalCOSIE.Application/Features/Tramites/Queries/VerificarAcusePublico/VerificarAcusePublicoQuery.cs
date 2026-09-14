using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Tramites.DTO;

namespace PortalCOSIE.Application.Features.Tramites.Queries.VerificarAcusePublico;

public sealed record VerificarAcusePublicoQuery(string Token) : IRequest<AcuseVerificacionPublicaDTO?>;