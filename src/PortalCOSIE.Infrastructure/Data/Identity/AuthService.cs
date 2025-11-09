using Microsoft.AspNetCore.Identity;
using PortalCOSIE.Application;
using PortalCOSIE.Application.DTO.Cuenta;
using PortalCOSIE.Application.Interfaces;

namespace PortalCOSIE.Infrastructure.Data.Identity
{
    /// <summary>
    /// Servicio de autenticación que maneja el ingreso y cierre de sesión de usuarios.
    /// </summary>
    public class AuthService: IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Result<string>> IngresarUsuarioAsync(IngresarDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Correo);

            if (user == null)
            {
                var errors = "Usuario no encontrado.";
                return Result<string>.Failure(errors);
            }

            if (!user.EmailConfirmed)
            {
                //En caso de que el ususario borre el email de confirmacion que?
                var errors = "Revisa tu correo electrónico para confirmar tu cuenta.";
                return Result<string>.Failure(errors);
            }

            //RememberMe false para mayor seguridad
            var result = await _signInManager.PasswordSignInAsync(user, dto.Contrasena, false, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                var lockoutEnd = await _userManager.GetLockoutEndDateAsync(user);
                var restante = lockoutEnd.Value.UtcDateTime - DateTime.UtcNow;
                int minutos = (int)restante.TotalMinutes;
                int segundos = restante.Seconds;

                string tiempo;
                if (minutos > 0)
                    tiempo = $"{minutos} minuto{(minutos > 1 ? "s" : "")} y {segundos} segundo{(segundos != 1 ? "s" : "")}";
                else
                    tiempo = $"{segundos} segundo{(segundos != 1 ? "s" : "")}";

                return Result<string>.Failure("Tu cuenta ha sido bloqueada por múltiples intentos fallidos.", $"Intenta de nuevo en {tiempo}.");
            }

            return result.Succeeded
                ? Result<string>.Success("Inicio de sesión exitoso.")
                : Result<string>.Failure("Contraseña incorrecta.");
        }
        public async Task CerrarSesionAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
