using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Carreras.Commands.EditarUnidad
{
    public class EditarUnidadHandler : IRequestHandler<EditarUnidadCommand, UnidadAprendizaje>
    {
        private readonly ICarreraRepository _carreraRepo;
        private readonly IUnitOfWork _unitOfWork;
        public EditarUnidadHandler(ICarreraRepository carreraRepository, IUnitOfWork unitOfWork)
        {
            _carreraRepo = carreraRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UnidadAprendizaje> Handle(EditarUnidadCommand command)
        {
            var carrera = await _carreraRepo.ObtenerCarreraConUnidadesAsync(command.carreraId);
            if (carrera == null)
                throw new ApplicationException("Carrera no encontrada");

            var unidad = carrera.UnidadesAprendizaje
                .FirstOrDefault(u => u.Id == command.unidadId);

            if (unidad == null)
                throw new ApplicationException("Unidad de aprendizaje no encontrada");

            unidad.SetNombre(command.nombre);
            unidad.SetSemestre(command.semestre);

            await _unitOfWork.SaveChangesAsync();
            return unidad;
        }
    }
}
