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
                throw new ApplicationException("El número de boleta ya existe");
            
            var usuario = await _usuarioRepo.BuscarConAlumno(dto.IdentityUserId);
            if (usuario == null)
                throw new ApplicationException("Usuario no encontrado");

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
            
            return cambios > 0
                ? Result<string>.Success("Usuario actualizado con éxito")
                : Result<string>.Success("No se detectaron cambios para guardar");
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