using PortalCOSIE.Application.Abstractions;
using PortalCOSIE.Application.Features.Usuarios.DTO;

namespace PortalCOSIE.Application.Features.Usuarios.Queries.ListarAlumnos
{
    public sealed record ListarAlumnosQuery() : IRequest<IEnumerable<AlumnoDTO>>;
}
