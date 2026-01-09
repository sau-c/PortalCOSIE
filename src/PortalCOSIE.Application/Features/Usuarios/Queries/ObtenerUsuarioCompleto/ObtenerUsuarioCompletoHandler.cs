using PortalCOSIE.Application.Features.Usuarios.DTO;
using PortalCOSIE.Application.Services.Query;

namespace PortalCOSIE.Application.Features.Usuarios.Queries.ObtenerAlumnoCompleto
{
    public class ObtenerUsuarioCompletoHandler : IRequestHandler<ObtenerUsuarioCompletoQuery, UsuarioDTO>
    {
        private readonly IUsuarioQueryService _queryService;
        public ObtenerUsuarioCompletoHandler(IUsuarioQueryService queryService)
            => _queryService = queryService;
        public async Task<UsuarioDTO> Handle(ObtenerUsuarioCompletoQuery query)
            => await _queryService.ObtenerUsuarioCompletoPorId(query.identityUserId);
    }
}
