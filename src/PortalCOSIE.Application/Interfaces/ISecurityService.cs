using PortalCOSIE.Application.DTO.Cuenta;
using PortalCOSIE.Application.DTO.Usuario;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ISecurityService
    {
        Task<Result<string>> IngresarUsuarioAsync(IngresarDTO dto);
        Task CerrarSesionAsync();
        Task<Result<string>> CrearUsuarioAsync(CrearCuentaDTO dto);
        Task<Result<string>> ConfirmarCorreoAsync(string correo, string token);
        Task<Result<string>> RecuperarContrasenaAsync(string correo);
        Task<Result<string>> RestablecerContrasenaAsync(RestablecerDTO dto);
        Task<Result<string>> CambiarContrasena(CambiarContrasenaDTO dto);
        Task<Result<string>> ToggleRol(string userId, string rol);
        Task<Result<string>> VerificarCorreoAsync(string userId, string correo);
        Task<Result<string>> ActualizarCorreoAsync(string id, string correo, string token);
        Task<Result<string>> ActualizarCelularAsync(string userId, string celular);
        Task<AlumnoCompletoDTO?> BuscarAlumnoCompleto(string identityUserId);
        Task<IEnumerable<AlumnoCompletoDTO>> ListarAlumnos();
        Task<IEnumerable<PersonalCompletoDTO>> ListarPersonal();
    }
}
