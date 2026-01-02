using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Carreras.Commands.Editar
{
    public class EditarCarreraHandler : IRequestHandler<EditarCarreraCommand, Carrera>
    {
        private readonly ICarreraRepository _carreraRepo;
        private readonly IUnitOfWork _unitOfWork;

        public EditarCarreraHandler(ICarreraRepository carreraRepo, IUnitOfWork unitOfWork)
        {
            _carreraRepo = carreraRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Carrera> Handle(EditarCarreraCommand command)
        {
            var carrera = await _carreraRepo.GetByIdAsync(command.carreraId);
            if (carrera == null)
                throw new ApplicationException("Carrera no encontrada");
            carrera.ActualizarNombre(command.nombre);
            await _unitOfWork.SaveChangesAsync();
            return carrera;
        }
    }
}