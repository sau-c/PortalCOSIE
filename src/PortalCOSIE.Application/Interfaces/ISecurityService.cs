using PortalCOSIE.Application.DTO.Cuenta;
using PortalCOSIE.Application.DTO.Usuario;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ISecurityService
    {
        Task<Result<string>> RegistrarAlumnoPendiente(RegistrarAlumnoDTO dto);
        Task<Result<string>> CrearContrasenaAsync(CrearContrasenaDTO dto);
        Task<Result<string>> ConfirmarCorreoAsync(string correo, string token);
        Task<Result<string>> RecuperarContrasenaAsync(CorreoDTO dto);
        Task<Result<string>> RestablecerContrasenaAsync(RestablecerDTO dto);
        Task<Result<string>> ToggleRol(string userId, string rol);
        Task<Result<string>> ActualizarCorreoAsync(string userId, string correo);
        Task<IEnumerable<AlumnoConIdentityDTO>> ListarAlumnos();
        Task<IEnumerable<PersonalConIdentityDTO>> ListarPersonal();
    }
}
