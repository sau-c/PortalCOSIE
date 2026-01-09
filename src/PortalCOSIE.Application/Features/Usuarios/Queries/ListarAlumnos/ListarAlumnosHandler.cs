using PortalCOSIE.Application.Features.Usuarios.DTO;
using PortalCOSIE.Application.Services.Query;

namespace PortalCOSIE.Application.Features.Usuarios.Queries.ListarAlumnos
{
    public class ListarAlumnosHandler : IRequestHandler<ListarAlumnosQuery, IEnumerable<AlumnoDTO>>
    {
        private readonly IUsuarioQueryService _queryService;
        public ListarAlumnosHandler(IUsuarioQueryService queryService)
            => _queryService = queryService;
        
        public async Task<IEnumerable<AlumnoDTO>> Handle(ListarAlumnosQuery request)
            => await _queryService.ListarAlumnosCompletos();
    }
}
