using PortalCOSIE.Application;
using PortalCOSIE.Application.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Identity;
using PortalCOSIE.Application.Services;

namespace PortalCOSIE.Infrastructure.Data.Identity
{
    public class CuentaCorreoService : ICuentaCorreoService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public CuentaCorreoService(
            UserManager<IdentityUser> userManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<Result<string>> ConfirmarCorreoAsync(string correo, string token)
        {
            var user = await _userManager.FindByEmailAsync(correo);
            if (user == null)
                return Result<string>.Failure("Usuario no encontrado");
            if (user.EmailConfirmed)
                return Result<string>.Failure("Ya se ha confirmado tu correo");
            if (token == null)
                return Result<string>.Failure("Token vacio");
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return Result<string>.Failure(errors);
            }
            return Result<string>.Success("Correo confirmado");
        }
        public async Task<Result<string>> VerificarCorreoAsync(string userId, string correo)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(userId))
                return Result<string>.Failure("El id no puede ser nulo o vacío");
            if (string.IsNullOrWhiteSpace(correo))
                return Result<string>.Failure("El correo no puede ser nulo o vacío");

            // Buscar usuario
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Result<string>.Failure("No se encontró el usuario a editar");

            // Si es el mismo correo -> no hacer nada
            if (user.NormalizedEmail == _userManager.NormalizeEmail(correo))
                return Result<string>.Failure("No se detectaron cambios");

            // Validar si el correo ya está en uso
            var correoExistente = await _userManager.FindByEmailAsync(correo);
            if (correoExistente != null)
                return Result<string>.Failure("Ese correo ya está en uso");

            // 2. Envio correo
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, correo);
            var encodedToken = WebUtility.UrlEncode(token);
            var envio = await _emailSender.SendEmailAsync(correo, "Cambio de correo", HtmlTemplates.VerificarCorreoHtml(userId, correo, encodedToken));
            if (!envio.Succeeded)
                return Result<string>.Failure("No se pudo enviar el correo.");

            return Result<string>.Success($"Se envió una verificación a {correo}");
        }
        public async Task<Result<string>> ActualizarCorreoAsync(string id, string correo, string token)
        {
            // 1. Validaciones
            if (string.IsNullOrEmpty(correo))
                return Result<string>.Failure("El nuevo correo no puede ser nulo o vacio");
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return Result<string>.Failure("Usuario no encontrado");
            string correoViejo = user.Email;

            var emailResult = await _userManager.ChangeEmailAsync(user, correo, token);
            if (!emailResult.Succeeded)
                return Result<string>.Failure(string.Join(", ", emailResult.Errors.Select(e => e.Description)));

            var nameResult = await _userManager.SetUserNameAsync(user, correo);
            if (!nameResult.Succeeded)
                return Result<string>.Failure($"Error al actualizar nombre de usuario: {string.Join(", ", nameResult.Errors.Select(e => e.Description))}");

            var envio = await _emailSender.SendEmailAsync(correoViejo, "Correo actualizado", HtmlTemplates.CorreoActualizadoHtml(correo));
            if (!envio.Succeeded)
                return Result<string>.Failure("No se pudo enviar el correo.");

            return Result<string>.Success("Se actualizó el correo con éxito");
        }
    }
}
