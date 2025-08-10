using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Application;
using PortalCOSIE.Application.DTO.Cuenta;
using PortalCOSIE.Application.DTO.Rol;
using PortalCOSIE.Application.DTO.Usuario;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;
using PortalCOSIE.Infrastructure.Data.Email;
using System.Data;
using System.Net;

namespace PortalCOSIE.Infrastructure.Data.Identity
{
    /// <summary>
    /// Servicio de seguridad que maneja la creación de usuarios, confirmación de correos, recuperación de contraseñas y gestión de roles.
    /// </summary>
    public class SecurityService : ISecurityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IGenericRepo<Usuario> _usuarioRepository;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecurityService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IGenericRepo<Usuario> usuarioRepository,
            IEmailSender emailSender,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _usuarioRepository = usuarioRepository;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<string>> CrearUsuarioAsync(CrearCuentaDTO dto)
        {
            var user = new IdentityUser
            {
                UserName = dto.Correo,
                Email = dto.Correo,
            };

            var crear = await _userManager.CreateAsync(user, dto.Contrasena);
            if (!crear.Succeeded)
            {
                var errors = crear.Errors.Select(e => e.Description);
                return Result<string>.Failure(errors);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return Result<string>.Failure("No se pudo determinar el origen del contexto HTTP");
            var dominio = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
            var url = $"{dominio}/Cuenta/Confirmar?correo={dto.Correo}&token={encodedToken}";

            var correo = await _emailSender.SendEmailAsync(dto.Correo, "Confirma tu cuenta", HtmlTemplates.ConfirmarCorreoHtml(url));

            if (!correo.Succeeded)
            {
                var errors = crear.Errors.Select(e => e.Description);
                return Result<string>.Failure(errors);
            }
            return Result<string>.Success("Se ha enviado un enlace a tu correo para confirmar tu cuenta. No olvides revisar tu carpeta de spam.");
        }

        public async Task<Result<string>> ConfirmarCorreoAsync(string correo, string token)
        {
            var user = await _userManager.FindByEmailAsync(correo);
            if (user == null)
            {
                return Result<string>.Failure("Usuario no encontrado");
            }

            if (user.EmailConfirmed)
            {
                return Result<string>.Failure("Ya se ha confirmado tu correo");
            }

            if (token == null)
            {
                return Result<string>.Failure("Token vacio");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return Result<string>.Failure(errors);
            }
            return Result<string>.Success("Correo confirmado");
        }

        public async Task<Result<string>> RecuperarContrasenaAsync(CorreoDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Correo);

            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                return Result<string>.Failure("No se puede restablecer la contraseña. Verifica que el correo sea correcto y esté confirmado.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return Result<string>.Failure("No se pudo determinar el origen del contexto HTTP");

            var dominio = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";

            var url = $"{dominio}/Cuenta/Restablecer?correo={dto.Correo}&token={encodedToken}";

            var envio = await _emailSender.SendEmailAsync(
                dto.Correo,
                "Restablece tu contraseña",
                HtmlTemplates.RecuperarContrasenaHtml(url)
            );

            if (!envio.Succeeded)
                return Result<string>.Failure("No se pudo enviar el correo de recuperación.");

            return Result<string>.Success("Se ha enviado un enlace para restablecer tu contraseña.");
        }

        public async Task<Result<string>> RestablecerContrasenaAsync(RestablecerDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Correo);
            if (user == null)
                return Result<string>.Failure("No se encontró el usuario.");

            var resultado = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NuevaContrasena);

            if (!resultado.Succeeded)
            {
                var errors = resultado.Errors.Select(e => e.Description);
                return Result<string>.Failure(errors);
            }

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return Result<string>.Failure("No se pudo determinar el origen del contexto HTTP");

            var dominio = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";

            var url = $"{dominio}/Cuenta/Recuperar";

            var envio = await _emailSender.SendEmailAsync(
                dto.Correo,
                "Tu contraseña ha sido cambiada con éxito",
                HtmlTemplates.ContrasenaCambiadaHtml(url)
            );

            if (!envio.Succeeded)
                return Result<string>.Failure("No se pudo enviar el correo de aviso.");

            return Result<string>.Success("Contraseña restablecida con éxito.");
        }

        public async Task<IEnumerable<UsuarioDTO>> ListarUsuarios()
        {
            var usuarios = _usuarioRepository.GetAll().ToList();
            var usuariosDTO = new List<UsuarioDTO>();

            foreach (var usuario in usuarios)
            {
                var identityUser = await _userManager.FindByIdAsync(usuario.IdentityUserId);
                if (identityUser == null) continue;

                var roles = await _userManager.GetRolesAsync(identityUser);
                var rol = roles.FirstOrDefault() ?? "Sin rol";

                usuariosDTO.Add(new UsuarioDTO
                {
                    IdentityUserId = identityUser.Id,
                    Nombre = usuario.Nombre,
                    ApellidoPaterno = usuario.ApellidoPaterno,
                    ApellidoMaterno = usuario.ApellidoMaterno,
                    Correo = identityUser.Email,
                    Rol = rol
                });
            }

            return usuariosDTO;
        }

        public async Task<Result<string>> EliminarUsuario(string id)
        {
            // 1. Validaciones de entrada
            if (string.IsNullOrEmpty(id))
                return Result<string>.Failure("Se requiere ID de usuario");
            // 2. Obtener usuario
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return Result<string>.Failure("Usuario no encontrado");
            // 3. Operación de eliminación
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return Result<string>.Failure($"Error al eliminar usuario: {result.Errors}");
            return Result<string>.Success("Usuario eliminado con éxito");
        }

        public async Task<Result<string>> EditarUsuario(UsuarioDTO dto)
        {
            // 1. Validaciones de entrada
            if (dto == null || string.IsNullOrEmpty(dto.IdentityUserId))
                return Result<string>.Failure("Se requiere ID de usuario");
            // 2. Obtener usuario
            var user = await _userManager.FindByIdAsync(dto.IdentityUserId);
            if (user == null)
                return Result<string>.Failure("Usuario no encontrado");
            // 3. Actualizar propiedades del usuario
            user.UserName = dto.Correo;
            user.Email = dto.Correo;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return Result<string>.Failure($"Error al actualizar usuario: {result.Errors}");
            // 4. Actualizar datos adicionales si es necesario
            //var usuario = await _usuarioRepository.GetById(dto.IdentityUserId);
            //if (usuario != null)
            //{
            //    usuario.Nombre = dto.Nombre;
            //    usuario.ApellidoPaterno = dto.ApellidoPaterno;
            //    usuario.ApellidoMaterno = dto.ApellidoMaterno;
            //    await _usuarioRepository.UpdateAsync(usuario);
            //}
            return Result<string>.Success("Usuario actualizado con éxito");
        }

        public async Task<IEnumerable<RolConClaimsDTO>> ListarRoles()
        {
            var roles = _roleManager.Roles.ToList();
            var rolesData = new List<RolConClaimsDTO>();

            foreach (var role in roles)
            {
                var claims = await _roleManager.GetClaimsAsync(role);
                rolesData.Add(new RolConClaimsDTO
                {
                    Id = role.Id,
                    Nombre = role.Name
                    //,
                    //Claims = claims.Select(c => new ClaimViewModel
                    //{
                    //    Type = c.Type,
                    //    Value = c.Value
                    //}).ToList()
                });
            }

            return rolesData;
        }
    }
}
