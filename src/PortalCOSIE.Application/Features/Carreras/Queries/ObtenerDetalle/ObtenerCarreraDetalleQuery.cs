using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.Carreras;

namespace PortalCOSIE.Application.Features.Carreras.Queries.ObtenerDetalle
{
    public sealed record ObtenerCarreraDetalleQuery(int carreraId) : IRequest<Carrera>;
}
