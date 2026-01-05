using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Tramites.DTO;

namespace PortalCOSIE.Application.Features.Tramites.Queries.DescargarDocumento
{
    public sealed record DescargarDocumentoQuery(
        string IdentityUserId,
        string Rol,
        int DocumentoId
        ) : IRequest<ArchivoDTO>;
}
