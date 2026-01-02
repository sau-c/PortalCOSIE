using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Tramites.Queries.ListarEstadosTramite
{
    public class ListarEstadoTramiteHandler : IRequestHandler<ListarEstadoTramiteQuery, IEnumerable<EstadoTramite>>
    {
        private readonly IBaseRepository<EstadoTramite, int> _estadoTramiteRepo;
        public ListarEstadoTramiteHandler(IBaseRepository<EstadoTramite, int> estadoTramiteRepo)
            => _estadoTramiteRepo = estadoTramiteRepo;

        public async Task<IEnumerable<EstadoTramite>> Handle(ListarEstadoTramiteQuery query)
            => await _estadoTramiteRepo.GetAllAsync();
    }
}
