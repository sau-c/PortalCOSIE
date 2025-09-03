using PortalCOSIE.Application.DTO.Cuenta;
using PortalCOSIE.Application.DTO.Usuario;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;


namespace PortalCOSIE.Application
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepo<Usuario> _usuarioRepository;

        public UsuarioService(IUnitOfWork unitOfWork, IGenericRepo<Usuario> usuarioRepository)
        {
            _unitOfWork = unitOfWork;
            _usuarioRepository = usuarioRepository;
        }

        public Usuario? BuscarUsuarioPorIdentityId(string id)
        {
            return _usuarioRepository.Query().Where(u => u.IdentityUserId == id).FirstOrDefault();
            //return _usuarioRepository.Get(u=>u.IdentityUserId == id);
        }
        
        public async Task<Result<string>> RegistrarAlumno(RegistrarDTO dto, string userId)
        {
            var usuario = new Usuario
            {
                IdentityUserId = userId,
                Nombre = dto.Nombre,
                ApellidoPaterno = dto.ApellidoPaterno,
                ApellidoMaterno = dto.ApellidoMaterno
            };

            await _unitOfWork.GenericRepo<Usuario>().AddAsync(usuario);
            await _unitOfWork.CompleteAsync();
            var alumno = new Alumno
            {
                Id = usuario.Id,
                NumeroBoleta = dto.NumeroBoleta,
                FechaIngreso = dto.FechaIngreso,
                CarreraId = dto.CarreraId
            };
            await _unitOfWork.GenericRepo<Alumno>().AddAsync(alumno);
            await _unitOfWork.CompleteAsync();
            return Result<string>.Success(alumno.NumeroBoleta);
        }

        public async Task<AlumnoDTO?> BuscarAlumno(string id)
        {
            var usuario = await _usuarioRepository.GetFirstOrDefaultAsync(
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
                FechaIngreso = usuario.Alumno.FechaIngreso,
                CarreraId = usuario.Alumno.CarreraId,
                CarreraNombre = usuario.Alumno.Carrera?.Nombre
            };
        }

        public async Task<Result<string>> EditarAlumno(AlumnoDTO dto)
        {
            // 1. Validar que el DTO tenga el IdentityUserId
            if (string.IsNullOrEmpty(dto.IdentityUserId))
                return Result<string>.Failure("Se requiere el IdentityUserId");

            // 2. Obtener datos existentes
            var usuario = await _unitOfWork.GenericRepo<Usuario>()
                .GetFirstOrDefaultAsync(
                    u => u.IdentityUserId == dto.IdentityUserId,
                    u => u.Alumno);

            if (usuario == null)
                return Result<string>.Failure("Usuario no encontrado");

            if (usuario.Alumno == null)
                return Result<string>.Failure("Registro de alumno no encontrado");

            // 3. Actualizar solo los campos modificados (patch update)
            usuario.Nombre = dto.Nombre ?? usuario.Nombre;
            usuario.ApellidoPaterno = dto.ApellidoPaterno ?? usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = dto.ApellidoMaterno ?? usuario.ApellidoMaterno;

            // 4. Actualizar datos del alumno
            var alumno = usuario.Alumno;
            alumno.NumeroBoleta = dto.NumeroBoleta ?? alumno.NumeroBoleta;
            alumno.CarreraId = dto.CarreraId ?? alumno.CarreraId;

            // Solo actualizar fecha si es diferente a default
            if (dto.FechaIngreso != default)
            {
                alumno.FechaIngreso = dto.FechaIngreso;
            }

            // 4. Persistir cambios
            await _unitOfWork.GenericRepo<Usuario>().UpdateAsync(usuario);
            var cambios = await _unitOfWork.CompleteAsync();

            return cambios > 0
                ? Result<string>.Success("Usuario actualizado con éxito")
                : Result<string>.Failure("No se detectaron cambios para guardar");
        }
    }
}