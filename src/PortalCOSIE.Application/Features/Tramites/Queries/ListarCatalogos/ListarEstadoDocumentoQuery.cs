using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.Documentos;

namespace PortalCOSIE.Application.Features.Tramites.Queries.ListarEstadosTramite
{
    public sealed record ListarEstadoDocumentoQuery : IRequest<IEnumerable<EstadoDocumento>>;
}
