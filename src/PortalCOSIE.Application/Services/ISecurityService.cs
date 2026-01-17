using PortalCOSIE.Application.Features.Usuarios.DTO;

namespace PortalCOSIE.Application.Services
{
    public interface ISecurityService
    {
        Task<Result<string>> IngresarUsuarioAsync(IngresarDTO dto);
        Task CerrarSesionAsync();
        Task<Result<string>> CrearUsuario(CrearCuentaDTO dto);
        Task<Result<string>> CrearPersonal(CrearPersonalDTO dto);
        Task<Result<string>> RecuperarContrasena(string correo);
        Task<Result<string>> RestablecerContrasena(RestablecerDTO dto);
        Task<Result<string>> CambiarContrasena(CambiarContrasenaDTO dto);
        Task<Result<string>> ToggleRol(string userId, string rol);
        Task<Result<string>> ActualizarCelular(string userId, string celular);

        Task<Result<string>> ConfirmarCorreo(string correo, string token);
        Task<Result<string>> VerificarCorreo(string userId, string correo);
        Task<Result<string>> ActualizarCorreo(string id, string correo, string token);
    }
}
