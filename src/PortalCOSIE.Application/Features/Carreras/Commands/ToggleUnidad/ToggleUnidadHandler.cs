using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Carreras.Commands.ToggleUnidad
{
    public class ToggleUnidadHandler : IRequestHandler<ToggleUnidadCommand, UnidadAprendizaje>
    {
        private readonly ICarreraRepository _carreraRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ToggleUnidadHandler(ICarreraRepository carreraRepo, IUnitOfWork unitOfWork)
        {
            _carreraRepo = carreraRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<UnidadAprendizaje> Handle(ToggleUnidadCommand request)
        {
            var carrera = await _carreraRepo.ObtenerCarreraConUnidadesAsync(request.carreraId);
            if (carrera == null)
                throw new ApplicationException("Carrera no encontrada");

            var unidad = carrera.UnidadesAprendizaje
                .FirstOrDefault(u => u.Id == request.unidadId);

            if (unidad.IsDeleted)
                unidad.Restore();
            else
                unidad.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
            return unidad;
        }
    }
}