using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Carreras.Commands.Toggle
{
    public class ToggleCarreraHandler : IRequestHandler<ToggleCarreraCommand, Carrera>
    {
        private readonly ICarreraRepository _carreraRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ToggleCarreraHandler(ICarreraRepository carreraRepo, IUnitOfWork unitOfWork)
        {
            _carreraRepo = carreraRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Carrera> Handle(ToggleCarreraCommand request)
        {
            var carrera = await _carreraRepo.GetByIdAsync(request.id);
            if (carrera.IsDeleted)
                carrera.Restore();
            else
                carrera.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
            return carrera;
        }
    }
}