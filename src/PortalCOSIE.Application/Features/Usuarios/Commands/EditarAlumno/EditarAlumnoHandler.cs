using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.Usuarios.Commands.EditarAlumno
{
    public class EditarAlumnoHandler : IRequestHandler<EditarAlumnoCommand, Result<string>>
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IUnitOfWork _unitOfWork;
        public EditarAlumnoHandler(IUsuarioRepository usuarioRepo, IUnitOfWork unitOfWork)
        {
            _usuarioRepo = usuarioRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(EditarAlumnoCommand command)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var alumnoPorBoleta = await _usuarioRepo.BuscarAlumnoPorBoleta(command.NumeroBoleta);
                if (alumnoPorBoleta != null && alumnoPorBoleta.IdentityUserId != command.IdentityUserId)
                    throw new ApplicationException("El número de boleta ya existe");
                var alumno = await _usuarioRepo.BuscarAlumnoConCarrera(command.IdentityUserId);

                alumno.SetNombre(command.Nombre);
                alumno.SetApellidoPaterno(command.ApellidoPaterno);
                alumno.SetApellidoMaterno(command.ApellidoMaterno);
                alumno.SetNumeroBoleta(command.NumeroBoleta);
                alumno.SetPeriodoIngreso(command.PeriodoIngreso);
                alumno.SetCarrera(command.CarreraId);

                var cambios = await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                if (cambios < 0)
                    Result<string>.Success("No se detectaron cambios para guardar");

                //var identityUser = await _userManager.FindByIdAsync(dto.IdentityUserId);
                //var envio = await _emailSender.SendEmailAsync(
                //    identityUser.Email,
                //    "Actualizamos tu información",
                //    HtmlTemplates.ActualizamosTuInformacion()
                //    );
                return Result<string>.Success("Usuario actualizado con éxito");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
