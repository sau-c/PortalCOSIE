using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Tramites.Commands.Cancelar
{
    public class CancelarTramiteHandler : IRequestHandler<CancelarTramiteCommand, Result<string>>
    {
        private readonly ITramiteRepository _tramiteRepo;
        private readonly IUnitOfWork _unitOfWork;
        public CancelarTramiteHandler(
            ITramiteRepository tramiteRepo,
            IUnitOfWork unitOfWork
            )
        {
            _tramiteRepo = tramiteRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CancelarTramiteCommand command)
        {
            var tramite = await _tramiteRepo.ObtenerTramiteCTCEPorIdParaRevision(command.tramiteId);
            if (tramite is null)
                return Result<string>.Failure("Trámite no encontrado.");

            tramite.AgregarObservaciones(command.observaciones);
            tramite.CambiarEstado(EstadoTramite.Cancelado);

            await _unitOfWork.SaveChangesAsync();
            return Result<string>.Success("Trámite cancelado.");
        }
    }
}
