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
                throw new ApplicationException("Usuario no encontrado.");
            }

            if (!user.EmailConfirmed)
            {
                //En caso de que el ususario borre el email de confirmacion que?
                return Result<string>.Failure("Revisa tu correo electrónico para confirmar tu cuenta.");
            }

            //RememberMe false para mayor seguridad
            var login = await _signInManager.PasswordSignInAsync(user, dto.Contrasena, false, lockoutOnFailure: true);

            if (login.IsLockedOut)
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

                return Result<string>.Failure($"Cuenta bloqueada por múltiples intentos fallidos. Intenta de nuevo en {tiempo}.");
            }
            if (login.IsNotAllowed)
            {
                return Result<string>.Failure("Ingreso no permitido");
            }

            return login.Succeeded
                ? Result<string>.Success("Inicio de sesión exitoso.")
                : Result<string>.Failure("Contraseña incorrecta.");
        }
        public async Task CerrarSesionAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
