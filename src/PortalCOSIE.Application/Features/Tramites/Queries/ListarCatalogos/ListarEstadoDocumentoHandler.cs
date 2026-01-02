using PortalCOSIE.Domain.Entities.Documentos;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Tramites.Queries.ListarEstadosTramite
{
    public class ListarEstadoDocumentoHandler : IRequestHandler<ListarEstadoDocumentoQuery, IEnumerable<EstadoDocumento>>
    {
        private readonly IBaseRepository<EstadoDocumento, int> _estadoDocumentoRepo;
        public ListarEstadoDocumentoHandler(IBaseRepository<EstadoDocumento, int> estadoDocumentoRepo)
            => _estadoDocumentoRepo = estadoDocumentoRepo;

        public async Task<IEnumerable<EstadoDocumento>> Handle(ListarEstadoDocumentoQuery query)
            => await _estadoDocumentoRepo.GetAllAsync();
    }
}
