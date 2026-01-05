using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Tramites.DTO;

namespace PortalCOSIE.Application.Features.Tramites.Queries.DescargarDocumentosPorTramite
{
    public sealed record DescargarDocumentosPorTramiteQuery(
        string IdentityUserId,
        string Rol,
        int TramiteId
        ) : IRequest<ArchivoDTO>;
}
