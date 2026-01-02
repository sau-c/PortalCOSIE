using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.EntradaBitacoras;

namespace PortalCOSIE.Application.Features.Bitacoras.Queries.Listar
{
    public sealed record ListarBitacoraQuery() : IRequest<IEnumerable<EntradaBitacora>>;
}
