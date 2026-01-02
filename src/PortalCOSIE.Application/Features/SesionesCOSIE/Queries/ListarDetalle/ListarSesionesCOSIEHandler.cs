using PortalCOSIE.Domain.Entities.SesionesCOSIE;

namespace PortalCOSIE.Application.Features.SesionesCOSIE.Queries.ListarDetalle
{
    public class ListarSesionesCOSIEHandler : IRequestHandler<ListarSesionesCOSIEQuery, IEnumerable<SesionCOSIE>>
    {
        private readonly ISesionRepository _sesionRepo;
        public ListarSesionesCOSIEHandler(ISesionRepository sesionRepo)
        {
            _sesionRepo = sesionRepo;
        }
        public async Task<IEnumerable<SesionCOSIE>> Handle(ListarSesionesCOSIEQuery request)
        {
            return await _sesionRepo.ListarSesiones(request.IncluirEliminados);
        }
    }
}
