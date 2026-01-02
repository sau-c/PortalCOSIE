using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Tramites.DTO;

namespace PortalCOSIE.Application.Features.Tramites.Queries.ObtenerDocumentoPorId
{
    public sealed record ObtenerDocumentoPorIdQuery(
        int DocumentoId,
        string IdentityUserId
        ) : IRequest<ArchivoDescargaDTO>;
}
