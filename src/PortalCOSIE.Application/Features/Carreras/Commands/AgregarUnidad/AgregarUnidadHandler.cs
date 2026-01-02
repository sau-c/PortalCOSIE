using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Carreras.Commands.AgregarUnidad
{
    public class AgregarUnidadHandler : IRequestHandler<AgregarUnidadCommand, UnidadAprendizaje>
    {
        private readonly ICarreraRepository _carreraRepo;
        private readonly IUnitOfWork _unitOfWork;

        public AgregarUnidadHandler(ICarreraRepository carreraRepo, IUnitOfWork unitOfWork)
        {
            _carreraRepo = carreraRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<UnidadAprendizaje> Handle(AgregarUnidadCommand command)
        {
            Carrera carrera = await _carreraRepo.GetByIdAsync(command.carreraId);
            carrera.AgregarUnidad(command.unidadId, command.nombre, command.semestre);
            await _unitOfWork.SaveChangesAsync();
            return carrera.UnidadesAprendizaje
                .FirstOrDefault(u => u.Id == command.unidadId);
        }
    }
}