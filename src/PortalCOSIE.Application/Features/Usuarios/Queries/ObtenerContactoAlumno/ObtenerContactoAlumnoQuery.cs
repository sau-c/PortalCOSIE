using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Usuarios.DTO;

namespace PortalCOSIE.Application.Features.Usuarios.Queries.ObtenerContactoAlumno
{
    public sealed record ObtenerContactoAlumnoQuery(int AlumnoId) : IRequest<AlumnoContactoDTO?>;
}