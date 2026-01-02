using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Usuarios.Commands.EditarPersonal
{
    public class EditarPersonalHandler : IRequestHandler<EditarPersonalCommand, Result<string>>
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IUnitOfWork _unitOfWork;

        public EditarPersonalHandler(IUsuarioRepository usuarioRepo, IUnitOfWork unitOfWork)
        {
            _usuarioRepo = usuarioRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(EditarPersonalCommand command)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var usuario = await _usuarioRepo.BuscarUsuario(command.IdentityUserId);
                if (usuario == null)
                    throw new ApplicationException("Usuario no encontrado");

                usuario.SetNombre(command.Nombre);
                usuario.SetApellidoPaterno(command.ApellidoPaterno);
                usuario.SetApellidoMaterno(command.ApellidoMaterno);

                var cambios = await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return cambios > 0
                    ? Result<string>.Success("Usuario actualizado con éxito")
                    : Result<string>.Success("No se detectaron cambios para guardar");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
