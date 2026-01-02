using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Application.Features.Usuarios.Queries.ObtenerPersonal
{
    public sealed record ObtenerPersonalQuery(string IdentityUserId) : IRequest<Personal>;
}
