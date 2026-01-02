using PortalCOSIE.Domain.Entities.SesionesCOSIE;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.SesionesCOSIE.Commands.Toggle
{
    public class ToggleSesionCOSIEHandler : IRequestHandler<ToggleSesionCOSIECommand, SesionCOSIE>
    {
        private readonly ISesionRepository _sesionRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ToggleSesionCOSIEHandler(ISesionRepository sesionRepo, IUnitOfWork unitOfWork)
        {
            _sesionRepo = sesionRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<SesionCOSIE> Handle(ToggleSesionCOSIECommand command)
        {
            var sesion = await _sesionRepo.GetByIdAsync(command.id);
            if (sesion.IsDeleted)
                sesion.Restore();
            else
                sesion.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
            return sesion;
        }
    }
}
