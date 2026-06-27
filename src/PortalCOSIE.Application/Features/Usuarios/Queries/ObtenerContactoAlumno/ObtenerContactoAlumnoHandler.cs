using PortalCOSIE.Application.Features.Usuarios.DTO;
using PortalCOSIE.Application.Services.Query;

namespace PortalCOSIE.Application.Features.Usuarios.Queries.ObtenerContactoAlumno
{
    public class ObtenerContactoAlumnoHandler : IRequestHandler<ObtenerContactoAlumnoQuery, AlumnoContactoDTO?>
    {
        private readonly IUsuarioQueryService _queryService;

        public ObtenerContactoAlumnoHandler(IUsuarioQueryService queryService)
            => _queryService = queryService;

        public Task<AlumnoContactoDTO?> Handle(ObtenerContactoAlumnoQuery query)
            => _queryService.ObtenerContactoAlumnoPorId(query.AlumnoId);
    }
}