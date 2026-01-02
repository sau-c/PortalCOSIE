using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Usuarios.DTO;

namespace PortalCOSIE.Application.Features.Usuarios.Queries.ObtenerAlumnoCompleto
{
    public sealed record ObtenerAlumnoCompletoQuery(
        string identityUserId
        ) : IRequest<AlumnoDTO>;
}
