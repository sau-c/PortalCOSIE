using PortalCOSIE.Application.Features.Usuarios.DTO;
using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Usuarios.Queries.ObtenerAlumnoCompleto
{
    public class ObtenerAlumnoCompletoHandler : IRequestHandler<ObtenerAlumnoCompletoQuery, AlumnoDTO>
    {
        private readonly IUsuarioQueryService _queryService;
        public ObtenerAlumnoCompletoHandler(IUsuarioQueryService queryService)
            => _queryService = queryService;
        public async Task<AlumnoDTO> Handle(ObtenerAlumnoCompletoQuery query)
            => await _queryService.ObtenerAlumnoCompletoPorId(query.identityUserId);
    }
}
