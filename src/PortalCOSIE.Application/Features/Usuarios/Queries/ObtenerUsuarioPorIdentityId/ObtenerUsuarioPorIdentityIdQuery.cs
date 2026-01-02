using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Application.Features.Usuarios.Queries.ObtenerUsuarioPorIdentityId
{
    public sealed record ObtenerUsuarioPorIdentityIdQuery(string IdentityUserId) : IRequest<Usuario>;
}
