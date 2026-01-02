using PortalCOSIE.Domain.Entities.SesionesCOSIE;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.SesionesCOSIE.Commands.Crear
{
    public class CrearSesionCOSIEHandler : IRequestHandler<CrearSesionCOSIECommand, SesionCOSIE>
    {
        private readonly ISesionRepository _sesionRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CrearSesionCOSIEHandler(
            ISesionRepository sesionRepo,
            IUnitOfWork unitOfWork)
        {
            _sesionRepo = sesionRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<SesionCOSIE> Handle(CrearSesionCOSIECommand command)
        {
            var sesion = new SesionCOSIE(
                command.numeroSesion,
                command.fechaSesion
                );
            sesion.SetFechasRecepcion(command.fechasRecepcion);
            await _sesionRepo.AddAsync(sesion);
            await _unitOfWork.SaveChangesAsync();
            return sesion;
        }
    }
}