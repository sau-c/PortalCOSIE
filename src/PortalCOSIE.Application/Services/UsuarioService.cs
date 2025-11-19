using PortalCOSIE.Application.DTO.Cuenta;
using PortalCOSIE.Application.DTO.Usuario;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IEmailSender _emailSender;

        public UsuarioService(
            IUnitOfWork unitOfWork,
            IUsuarioRepository usuarioRepo,
            IEmailSender emailSender
            )
        {
            _unitOfWork = unitOfWork;
            _usuarioRepo = usuarioRepo;
            _emailSender = emailSender;
        }

        public async Task<Result<string>> RegistrarAlumno(RegistrarDTO dto, string userId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                if (await _usuarioRepo.BuscarAlumnoPorBoleta(dto.NumeroBoleta) != null)
                    return Result<string>.Failure("El número de boleta ya existe");
                if (await _usuarioRepo.BuscarPorIdentityId(userId) != null)
                    throw new ApplicationException("Este id de usuario ya existe");

                var alumno = new Alumno(
                    dto.NumeroBoleta,
                    dto.PeriodoIngreso,
                    dto.CarreraId
                    );

                var usuario = new Usuario(
                    userId,
                    dto.Nombre,
                    dto.ApellidoPaterno,
                    dto.ApellidoMaterno
                );

                usuario.SetAlumno(alumno);
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
        public async Task<Usuario> BuscarUsuarioPorIdentityId(string id)
        {
            if (id == null)
                throw new ApplicationException("No se puede buscar un Id nulo");
            return await _usuarioRepo.BuscarPorIdentityId(id);
        }
        public async Task<Result<string>> EditarPersonal(PersonalDTO dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var usuario = await _usuarioRepo.BuscarPorIdentityId(dto.IdentityUserId);
                if (usuario == null)
                    throw new ApplicationException("Usuario no encontrado");

                if (usuario.Personal == null)
                    throw new ApplicationException("Registro de personal no encontrado");

                usuario.SetNombre(dto.Nombre);
                usuario.SetApellidoPaterno(dto.ApellidoPaterno);
                usuario.SetApellidoMaterno(dto.ApellidoMaterno);

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
        public async Task<Result<string>> EditarAlumno(AlumnoDTO dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var usuario = await _usuarioRepo.BuscarAlumnoPorBoleta(dto.NumeroBoleta);
                if (usuario != null && (usuario.IdentityUserId != dto.IdentityUserId))
                    throw new ApplicationException("El número de boleta ya existe");

                if (usuario.Alumno == null)
                    throw new ApplicationException("Registro de alumno no encontrado");

                usuario.SetNombre(dto.Nombre);
                usuario.SetApellidoPaterno(dto.ApellidoPaterno);
                usuario.SetApellidoMaterno(dto.ApellidoMaterno);

                usuario.Alumno.SetNumeroBoleta(dto.NumeroBoleta);
                usuario.Alumno.SetPeriodoIngreso(dto.PeriodoIngreso);
                usuario.Alumno.SetCarrera(dto.CarreraId);

                var cambios = await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                if (cambios > 0)
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
        public async Task<PersonalDTO?> BuscarPersonal(string id)
        {
            var personal = await _usuarioRepo.BuscarPorIdentityId(id);

            if (personal == null)
                return null;

            return new PersonalDTO
            {
                IdentityUserId = id,
                Nombre = personal.Nombre,
                ApellidoPaterno = personal.ApellidoPaterno,
                ApellidoMaterno = personal.ApellidoMaterno
            };
        }
    }
}