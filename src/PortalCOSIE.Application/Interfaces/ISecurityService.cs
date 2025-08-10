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
        Task<IEnumerable<UsuarioDTO>> ListarUsuarios();
        Task<Result<string>> EliminarUsuario(string id);
        Task<Result<string>> EditarUsuario(UsuarioDTO dto);
        Task<IEnumerable<RolConClaimsDTO>> ListarRoles();
        //Task<Result<IEnumerable<RolConClaimsDTO>>> ListarRoles();
    }
}
