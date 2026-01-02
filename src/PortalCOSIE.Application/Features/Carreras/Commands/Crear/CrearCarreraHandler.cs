using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Carreras.Commands.Crear
{
    public class CrearCarreraHandler : IRequestHandler<CrearCarreraCommand, Carrera>
    {
        private readonly ICarreraRepository _carreraRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CrearCarreraHandler(ICarreraRepository carreraRepo, IUnitOfWork unitOfWork)
        {
            _carreraRepo = carreraRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Carrera> Handle(CrearCarreraCommand command)
        {
            Carrera carrera = new Carrera(command.nombre);
            await _carreraRepo.AddAsync(carrera);
            await _unitOfWork.SaveChangesAsync();
            return carrera;
        }
    }
}