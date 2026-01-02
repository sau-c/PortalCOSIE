using PortalCOSIE.Application.Services;
using PortalCOSIE.Domain.Entities.Carreras;

namespace PortalCOSIE.Application.Features.Carreras.Queries.ListarUnidades
{
    public class ListarUnidadesHandler : IRequestHandler<ListarUnidadesQuery, IEnumerable<UnidadAprendizaje>>
    {
        private readonly ICarreraRepository _carreraRepo;
        private readonly IUsuarioQueryService _queryService;

        public ListarUnidadesHandler(
            ICarreraRepository carreraRepo,
            IUsuarioQueryService queryService
            )
        {
            _carreraRepo = carreraRepo;
            _queryService = queryService;
        }

        public async Task<IEnumerable<UnidadAprendizaje>> Handle(ListarUnidadesQuery query)
        {
            int carreraId = await _queryService.ObtenerCarreraAlumnoPorId(query.identityUserId);
            return await _carreraRepo.ListarUnidadesPorCarreraAsync(carreraId);
        }
    }
}