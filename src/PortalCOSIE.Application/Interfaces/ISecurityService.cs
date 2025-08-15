using PortalCOSIE.Application.DTO.Cuenta;
using PortalCOSIE.Application.DTO.Usuario;
using PortalCOSIE.Application.DTO.Rol;

namespace PortalCOSIE.Application.Interfaces
{
    public interface ISecurityService
    {
        Task<Result<string>> CrearUsuarioAsync(CrearCuentaDTO dto);
        Task<Result<string>> ConfirmarCorreoAsync(string correo, string token);
        Task<Result<string>> RecuperarContrasenaAsync(CorreoDTO dto);
        Task<Result<string>> RestablecerContrasenaAsync(RestablecerDTO dto);
        Task<IEnumerable<RolConClaimsDTO>> ListarRoles();
        Task<Result<string>> EliminarUsuario(string id);
        Task<IEnumerable<AlumnoConIdentityDTO>> ListarAlumnos();
        //Task<Result<IEnumerable<RolConClaimsDTO>>> ListarRoles();
    }
}
