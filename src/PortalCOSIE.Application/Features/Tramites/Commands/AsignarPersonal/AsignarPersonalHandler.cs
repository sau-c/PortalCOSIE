using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Tramites.Commands.AsignarPersonal
{
    public class AsignarPersonalHandler : IRequestHandler<AsignarPersonalCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly ITramiteRepository _tramiteRepo;

        public AsignarPersonalHandler(
            IUsuarioRepository usuarioRepo,
            ITramiteRepository tramiteRepo,
            IUnitOfWork unitOfWork
            )
        {
            _usuarioRepo = usuarioRepo;
            _tramiteRepo = tramiteRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(AsignarPersonalCommand command)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var personal = await _usuarioRepo.BuscarPersonal(command.IdentityUserId);
                if (personal == null)
                    return Result<string>.Failure("El personal no existe."); 
                var tramite = await _tramiteRepo.GetByIdAsync(command.TramiteId);
                if (tramite.PersonalId != null)
                    return Result<string>.Failure("El trámite ya tiene personal asignado.");
                tramite.AsignarPersonal(personal.Id);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return Result<string>.Success("Se ha tomado el trámite.");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
