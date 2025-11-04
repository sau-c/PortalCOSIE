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

        public UsuarioService(
            IUnitOfWork unitOfWork,
            IUsuarioRepository usuarioRepo
            )
        {
            _unitOfWork = unitOfWork;
            _usuarioRepo = usuarioRepo;
        }

        public async Task<Usuario> BuscarUsuarioPorIdentityId(string id)
        {
            return await _usuarioRepo.BuscarPorIdentityId(id);
        }

        public async Task<Result<string>> RegistrarAlumno(RegistrarDTO dto, string userId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                if (await _usuarioRepo.BuscarAlumnoPorBoleta(dto.NumeroBoleta) != null)
                {
                    return Result<string>.Failure("El número de boleta ya existe");
                }

                var usuario = new Usuario(
                    userId,
                    dto.Nombre,
                    dto.ApellidoPaterno,
                    dto.ApellidoMaterno
                );

                var alumno = new Alumno(
                    dto.NumeroBoleta,
                    dto.PeriodoIngreso,
                    dto.CarreraId
                    );

                usuario.SetAlumno(alumno);
                await _usuarioRepo.AddAsync(usuario);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return Result<string>.Success(alumno.NumeroBoleta);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<AlumnoDTO?> BuscarAlumno(string identityUserId)
        {
            var usuario = await _usuarioRepo.BuscarConAlumnoYCarrera(identityUserId);

            if (usuario == null)
                return null;

            return new AlumnoDTO
            {
                IdentityUserId = identityUserId,
                Nombre = usuario.Nombre,
                ApellidoPaterno = usuario.ApellidoPaterno,
                ApellidoMaterno = usuario.ApellidoMaterno,
                NumeroBoleta = usuario.Alumno.NumeroBoleta,
                PeriodoIngreso = usuario.Alumno.PeriodoIngreso,
                CarreraId = usuario.Alumno.CarreraId,
                CarreraNombre = usuario.Alumno.Carrera?.Nombre
            };
        }

        public async Task<Result<string>> EditarAlumno(AlumnoDTO dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            var alumnoPorBoleta = await _usuarioRepo.BuscarAlumnoPorBoleta(dto.NumeroBoleta);

            if (alumnoPorBoleta != null && (alumnoPorBoleta.IdentityUserId != dto.IdentityUserId))
            {
                return Result<string>.Failure("El número de boleta ya existe");
            }

            var usuario = await _usuarioRepo.BuscarConAlumno(dto.IdentityUserId);
            if (usuario == null)
                return Result<string>.Failure("Usuario no encontrado");

            if (usuario.Alumno == null)
                return Result<string>.Failure("Registro de alumno no encontrado");

            usuario.SetNombre(dto.Nombre);
            usuario.SetApellidoPaterno(dto.ApellidoPaterno);
            usuario.SetApellidoMaterno(dto.ApellidoMaterno);

            //var alumno = usuario.Alumno;
            usuario.Alumno.SetNumeroBoleta(dto.NumeroBoleta);
            usuario.Alumno.SetPeriodoIngreso(dto.PeriodoIngreso);
            usuario.Alumno.SetCarrera(dto.CarreraId);

            // 4. Persistir cambios
            var cambios = await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            
            return cambios > 0
                ? Result<string>.Success("Usuario actualizado con éxito")
                : Result<string>.Failure("No se detectaron cambios para guardar");
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