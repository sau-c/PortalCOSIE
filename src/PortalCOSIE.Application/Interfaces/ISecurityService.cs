using PortalCOSIE.Application.DTO.Cuenta;
using PortalCOSIE.Application.DTO.Usuario;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ISecurityService
    {
        Task<Result<string>> IngresarUsuarioAsync(IngresarDTO dto);
        Task CerrarSesionAsync();
        Task<Result<string>> CrearUsuarioAsync(CrearCuentaDTO dto);
        Task<Result<string>> CrearPersonalAsync(CrearPersonalDTO dto);
        Task<Result<string>> RecuperarContrasenaAsync(string correo);
        Task<Result<string>> RestablecerContrasenaAsync(RestablecerDTO dto);
        Task<Result<string>> CambiarContrasena(CambiarContrasenaDTO dto);
        Task<Result<string>> ToggleRol(string userId, string rol);
        Task<Result<string>> ActualizarCelularAsync(string userId, string celular);
        Task<AlumnoDTO?> BuscarAlumnoCompleto(string identityUserId);
        Task<IEnumerable<AlumnoDTO>> ListarAlumnos();
        Task<IEnumerable<PersonalDTO>> ListarPersonal();
    }
}
