using PortalCOSIE.Domain.Entities.Carreras;

namespace PortalCOSIE.Application.Features.Carreras.Queries.ObtenerDetalle
{
    public class ObtenerCarreraDetalleHandler : IRequestHandler<ObtenerCarreraDetalleQuery, Carrera>
    {
        private readonly ICarreraRepository _carreraRepo;

        public ObtenerCarreraDetalleHandler(ICarreraRepository carreraRepo)
        {
            _carreraRepo = carreraRepo;
        }

        public async Task<Carrera> Handle(ObtenerCarreraDetalleQuery request)
        {
            return await _carreraRepo.ObtenerCarreraConUnidadesAsync(request.carreraId);
        }
    }
}