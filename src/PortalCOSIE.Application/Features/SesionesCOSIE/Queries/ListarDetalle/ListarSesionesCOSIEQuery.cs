using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.SesionesCOSIE;

namespace PortalCOSIE.Application.Features.SesionesCOSIE.Queries.ListarDetalle
{
    public sealed record ListarSesionesCOSIEQuery(bool IncluirEliminados = false) : IRequest<IEnumerable<SesionCOSIE>>;
}
