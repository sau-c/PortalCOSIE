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
    public class SecurityService : ISecurityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IGenericRepo<Usuario> _usuarioRepository;
        private readonly IEmailSender _emailSender;

        public SecurityService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IGenericRepo<Usuario> usuarioRepository,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _usuarioRepository = usuarioRepository;
            _emailSender = emailSender;
        }

        public async Task<Result<string>> CrearUsuarioAsync(CrearCuentaDTO dto)
        {
            var user = new IdentityUser
            {
                UserName = dto.Correo,
                Email = dto.Correo,
                PhoneNumber = dto.Celular
            };

            var crear = await _userManager.CreateAsync(user, dto.Contrasena);
            if (!crear.Succeeded)
            {
                var errors = crear.Errors.Select(e => e.Description);
                return Result<string>.Failure(errors);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);

            var correo = await _emailSender.SendEmailAsync(
                dto.Correo,
                "Confirma tu cuenta",
                HtmlTemplates.ConfirmarCorreoHtml(dto.Correo, encodedToken));

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
                return Result<string>.Failure("No se puede recuperar la contraseña. Verifica que el correo sea correcto y esté confirmado.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);

            var envio = await _emailSender.SendEmailAsync(
                dto.Correo,
                "Recupera tu contraseña",
                HtmlTemplates.RecuperarContrasenaHtml(dto.Correo, encodedToken)
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

            var envio = await _emailSender.SendEmailAsync(
                dto.Correo,
                "Contraseña restablecida con éxito",
                HtmlTemplates.ContrasenaRestablecidaHtml()
            );

            if (!envio.Succeeded)
                return Result<string>.Failure("No se pudo enviar el correo de aviso.");

            return Result<string>.Success("Contraseña restablecida con éxito.");
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
        public async Task<IEnumerable<AlumnoConIdentityDTO>> ListarAlumnos()
        {
            var usuarios = await _usuarioRepository.Query()
                .Where(u => u.Alumno != null)
                .Include(u => u.Alumno)
                .Include(u => u.Alumno.Carrera)
                .ToListAsync();

            var alumnosDTO = new List<AlumnoConIdentityDTO>();

            foreach (var usuario in usuarios)
            {
                var identityUser = await _userManager.FindByIdAsync(usuario.IdentityUserId);
                if (identityUser == null) continue;

                var roles = await _userManager.GetRolesAsync(identityUser);

                alumnosDTO.Add(new AlumnoConIdentityDTO
                {
                    IdentityUserId = identityUser.Id,
                    NumeroBoleta = usuario.Alumno?.NumeroBoleta ?? "N/A",
                    Nombre = usuario.Nombre,
                    ApellidoPaterno = usuario.ApellidoPaterno,
                    ApellidoMaterno = usuario.ApellidoMaterno,
                    Carrera = usuario.Alumno.Carrera.Nombre,
                    PeriodoIngreso = usuario.Alumno.PeriodoIngreso,
                    Correo = identityUser.Email,
                    Celular = identityUser.PhoneNumber,
                    Rol = roles.FirstOrDefault()
                });
            }

            return alumnosDTO;
        }
        public async Task<Result<string>> ToggleRol(string userId, string rol)
        {
            // Validaciones iniciales
            if (string.IsNullOrWhiteSpace(userId))
                return Result<string>.Failure("ID de usuario no válido");

            if (string.IsNullOrWhiteSpace(rol))
                return Result<string>.Failure("Nombre de rol no válido");

            // Roles válidos permitidos
            var validRoles = new[] { "Alumno", "Personal" };
            if (!validRoles.Contains(rol))
                return Result<string>.Failure($"Rol '{rol}' no es válido");

            // Obtener usuario
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Result<string>.Failure("Usuario no encontrado");

            // Verificar si el usuario tiene el rol actualmente
            var hasRole = await _userManager.IsInRoleAsync(user, rol);
            IdentityResult result;

            if (hasRole)
            {
                // Si tiene el rol, lo removemos (pasa a "Inactivo")
                result = await _userManager.RemoveFromRoleAsync(user, rol);

                return result.Succeeded
                    ? Result<string>.Success("Usuario marcado como Inactivo correctamente")
                    : Result<string>.Failure($"Error al desactivar usuario: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            else
            {
                // Si no tiene el rol, primero removemos cualquier rol conflictivo del mismo tipo
                var currentRoles = await _userManager.GetRolesAsync(user);
                var conflictingRoles = currentRoles.Where(r => validRoles.Contains(r)).ToList();

                // Remover roles conflictivos si existen
                if (conflictingRoles.Any())
                {
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, conflictingRoles);
                    if (!removeResult.Succeeded)
                    {
                        return Result<string>.Failure($"Error al remover roles existentes: {string.Join(", ", removeResult.Errors.Select(e => e.Description))}");
                    }
                }

                // Asignar el nuevo rol
                result = await _userManager.AddToRoleAsync(user, rol);

                return result.Succeeded
                    ? Result<string>.Success($"Rol {rol} asignado correctamente")
                    : Result<string>.Failure($"Error al asignar rol: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

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
        public async Task<IEnumerable<PersonalConIdentityDTO>> ListarPersonal()
        {
            var usuarios = await _usuarioRepository.Query()
                .Where(u => u.Personal != null)
                .Include(u => u.Personal)
                .ToListAsync();

            var personalDTO = new List<PersonalConIdentityDTO>();

            foreach (var usuario in usuarios)
            {
                var identityUser = await _userManager.FindByIdAsync(usuario.IdentityUserId);
                if (identityUser == null) continue;

                var roles = await _userManager.GetRolesAsync(identityUser);

                personalDTO.Add(new PersonalConIdentityDTO
                {
                    IdentityUserId = identityUser.Id,
                    Nombre = usuario.Nombre,
                    ApellidoPaterno = usuario.ApellidoPaterno,
                    ApellidoMaterno = usuario.ApellidoMaterno,
                    Correo = identityUser.Email,
                    Rol = roles.FirstOrDefault()
                });
            }

            return personalDTO;
        }
    }
}
