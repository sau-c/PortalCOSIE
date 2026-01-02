using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.Carreras;

namespace PortalCOSIE.Application.Features.Carreras.Queries.Listar
{
    public sealed record ListarCarrerasQuery(bool IncluirEliminados = false) : IRequest<IEnumerable<Carrera>>;
}
