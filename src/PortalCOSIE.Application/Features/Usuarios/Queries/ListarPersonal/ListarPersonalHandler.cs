using PortalCOSIE.Application.Features.Usuarios.DTO;
using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Application.Features.Usuarios.Queries.ListarPersonal
{
    public class ListarPersonalHandler : IRequestHandler<ListarPersonalQuery, IEnumerable<PersonalDTO>>
    {
        private readonly IUsuarioQueryService _queryService;
        public ListarPersonalHandler(IUsuarioQueryService queryService)
            => _queryService = queryService;

        public async Task<IEnumerable<PersonalDTO>> Handle(ListarPersonalQuery request)
            => await _queryService.ListarPersonalCompletos();
    }
}
