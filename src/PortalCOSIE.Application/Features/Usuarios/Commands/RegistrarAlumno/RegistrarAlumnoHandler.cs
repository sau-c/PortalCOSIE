using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Usuarios.Commands.RegistrarAlumno
{
    public class RegistrarAlumnoHandler : IRequestHandler<RegistrarAlumnoCommand, Result<string>>
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IUnitOfWork _unitOfWork;
        public RegistrarAlumnoHandler(IUsuarioRepository usuarioRepo, IUnitOfWork unitOfWork)
        {
            _usuarioRepo = usuarioRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(RegistrarAlumnoCommand command)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                if (await _usuarioRepo.BuscarAlumnoPorBoleta(command.NumeroBoleta) != null)
                    return Result<string>.Failure("El número de boleta ya existe");

                var usuario = new Alumno(
                    command.IdentityUserId,
                    command.Nombre,
                    command.ApellidoPaterno,
                    command.ApellidoMaterno,
                    command.NumeroBoleta,
                    command.PeriodoIngreso,
                    command.CarreraId
                    );

                await _usuarioRepo.AddAsync(usuario);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return Result<string>.Success("Registro exitoso.\nDebes acudir a gestión escolar con tu credencial para activar tu cuenta.");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
