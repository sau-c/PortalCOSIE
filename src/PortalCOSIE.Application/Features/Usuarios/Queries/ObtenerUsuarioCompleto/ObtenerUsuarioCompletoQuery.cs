using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Usuarios.DTO;

namespace PortalCOSIE.Application.Features.Usuarios.Queries.ObtenerAlumnoCompleto
{
    public sealed record ObtenerUsuarioCompletoQuery(
        string identityUserId
        ) : IRequest<UsuarioDTO>;
}
