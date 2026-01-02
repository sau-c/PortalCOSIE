using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Tramites.Queries.ListarEstadosTramite
{
    public class ListarTipoTramiteHandler : IRequestHandler<ListarTipoTramiteQuery, IEnumerable<TipoTramite>>
    {
        private readonly IBaseRepository<TipoTramite, int> _estadoTramiteRepo;
        public ListarTipoTramiteHandler(IBaseRepository<TipoTramite, int> estadoTramiteRepo)
            => _estadoTramiteRepo = estadoTramiteRepo;

        public async Task<IEnumerable<TipoTramite>> Handle(ListarTipoTramiteQuery query)
            => await _estadoTramiteRepo.GetAllAsync();
    }
}
