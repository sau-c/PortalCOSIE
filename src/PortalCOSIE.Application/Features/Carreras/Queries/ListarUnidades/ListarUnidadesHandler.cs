using PortalCOSIE.Domain.Entities.Carreras;

namespace PortalCOSIE.Application.Features.Carreras.Queries.ListarUnidades
{
    public class ListarUnidadesHandler : IRequestHandler<ListarUnidadesQuery, IEnumerable<UnidadAprendizaje>>
    {
        private readonly ICarreraRepository _carreraRepo;

        public ListarUnidadesHandler(ICarreraRepository carreraRepo)
        {
            _carreraRepo = carreraRepo;
        }

        public async Task<IEnumerable<UnidadAprendizaje>> Handle(ListarUnidadesQuery query)
        {
            return await _carreraRepo.ListarUnidadesPorCarreraAsync(query.carreraId);
        }
    }
}