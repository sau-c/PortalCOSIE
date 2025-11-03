using PortalCOSIE.Application.DTO.Cuenta;
using PortalCOSIE.Application.DTO.Usuario;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Interfaces;


namespace PortalCOSIE.Application
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Usuario> _usuarioRepository;

        public UsuarioService(IUnitOfWork unitOfWork, IBaseRepository<Usuario> usuarioRepository)
        {
            _unitOfWork = unitOfWork;
            _usuarioRepository = usuarioRepository;
        }

        public Usuario? BuscarUsuarioPorIdentityId(string id)
        {
            return _usuarioRepository.Query().Where(u => u.IdentityUserId == id).FirstOrDefault();
            //return _usuarioRepository.GetByIdAsync(id);
        }

        public async Task<Result<string>> RegistrarAlumno(RegistrarDTO dto, string userId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var alumnoPorBoleta = _unitOfWork.BaseRepo<Usuario>().Query().Where(
                u => u.Alumno.NumeroBoleta == dto.NumeroBoleta
                ).FirstOrDefault();

                if (alumnoPorBoleta != null)
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
                await _unitOfWork.BaseRepo<Usuario>().AddAsync(usuario);
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

        public async Task<AlumnoDTO?> BuscarAlumno(string id)
        {
            var usuario = await _usuarioRepository.GetFirstOrDefaultWhereAsync(
                u => u.IdentityUserId == id,
                u => u.Alumno,
                u => u.Alumno.Carrera
                );

            if (usuario == null)
                return null;

            return new AlumnoDTO
            {
                IdentityUserId = id,
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
            var alumnoPorBoleta = _usuarioRepository.Query().Where(
                u => u.Alumno.NumeroBoleta == dto.NumeroBoleta
                && u.IdentityUserId != dto.IdentityUserId
                ).FirstOrDefault();

            if (alumnoPorBoleta != null)
            {
                return Result<string>.Failure("El número de boleta ya existe");
            }

            if (string.IsNullOrEmpty(dto.IdentityUserId))
                return Result<string>.Failure("Se requiere el IdentityUserId");

            var usuario = await _unitOfWork.BaseRepo<Usuario>()
                .GetFirstOrDefaultWhereAsync(
                    u => u.IdentityUserId == dto.IdentityUserId,
                    u => u.Alumno);

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
            var personal = await _usuarioRepository.GetFirstOrDefaultWhereAsync(
                u => u.IdentityUserId == id
                );

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