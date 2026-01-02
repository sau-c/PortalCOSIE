using PortalCOSIE.Domain.Entities.Carreras;

namespace PortalCOSIE.Application.Features.Carreras.Queries.Listar
{
    public class ListarCarrerasHandler : IRequestHandler<ListarCarrerasQuery, IEnumerable<Carrera>>
    {
        private readonly ICarreraRepository _carreraRepo;

        public ListarCarrerasHandler(ICarreraRepository carreraRepo)
        {
            _carreraRepo = carreraRepo;
        }

        public async Task<IEnumerable<Carrera>> Handle(ListarCarrerasQuery request)
        {
            return await _carreraRepo.GetAllAsync(request.IncluirEliminados);
        }
    }
}