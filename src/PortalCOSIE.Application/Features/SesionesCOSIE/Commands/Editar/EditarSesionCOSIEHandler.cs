using PortalCOSIE.Domain.Entities.SesionesCOSIE;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.SesionesCOSIE.Commands.Editar
{
    public class EditarSesionCOSIEHandler : IRequestHandler<EditarSesionCOSIECommand, SesionCOSIE>
    {
        private readonly ISesionRepository _sesionRepo;
        private readonly IUnitOfWork _unitOfWork;

        public EditarSesionCOSIEHandler(ISesionRepository sesionRepo, IUnitOfWork unitOfWork)
        {
            _sesionRepo = sesionRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<SesionCOSIE> Handle(EditarSesionCOSIECommand command)
        {
            var sesion = await _sesionRepo.ObtenerConFechasRecepcion(command.id);

            if (sesion == null)
                throw new Exception("No se encontro el registro");
            sesion.SetNumeroSesion(command.numeroSesion);
            sesion.SetFechaSesion(command.fechaSesion);
            sesion.SetFechasRecepcion(command.fechasRecepcion);
            await _unitOfWork.SaveChangesAsync();
            return sesion;
        }
    }
}