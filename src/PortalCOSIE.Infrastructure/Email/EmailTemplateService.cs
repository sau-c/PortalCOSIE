using Microsoft.Extensions.Configuration;
using PortalCOSIE.Application.Services.Notificacion;

namespace PortalCOSIE.Infrastructure.Email
{
    public class EmailTemplateService : IMessageTemplateService
    {
        private readonly string _baseUrl;

        public EmailTemplateService(IConfiguration config)
        {
            _baseUrl = config["App:BaseUrl"]?.TrimEnd('/')
                ?? throw new InvalidOperationException("App:BaseUrl no está configurado.");
        }

        private string Url(string path) => $"{_baseUrl}{path}";

        public string ConfirmarCorreo(string correo, string encodedToken) =>
            EmailLayout.Render(
                "Confirma tu correo",
                """
                <p style="margin-bottom: 20px;">
                    Para completar tu petición, por favor confirma tu correo electrónico haciendo clic en el siguiente botón:
                </p>
                <p style="margin-top: 30px; color: #888888; font-size: 14px;">
                    Si no creaste una cuenta, puedes ignorar este correo.
                </p>
                """,
                new EmailButton("Confirmar cuenta", Url($"/Cuenta/Confirmar?correo={correo}&token={encodedToken}")));

        public string RecuperarContrasena(string correo, string encodedToken) =>
            EmailLayout.Render(
                "Restablece tu contraseña",
                """
                <p style="margin-bottom: 20px;">
                    Hemos recibido una solicitud para restablecer tu contraseña. Si realizaste esta solicitud, haz clic en el botón de abajo para continuar:
                </p>
                <p style="margin-top: 30px; color: #888888; font-size: 14px;">
                    Si no solicitaste este cambio, puedes ignorar este correo. Tu contraseña actual permanecerá sin cambios.
                </p>
                """,
                new EmailButton("Restablecer contraseña", Url($"/Cuenta/Restablecer?correo={correo}&token={encodedToken}")));

        public string ContrasenaRestablecida() =>
            EmailLayout.Render(
                "Tu contraseña ha sido cambiada con éxito",
                """
                <p style="margin-bottom: 20px;">
                    Queremos informarte que la contraseña de tu cuenta fue cambiada recientemente. Si fuiste tú quien realizó este cambio, no necesitas hacer nada más. Si no reconoces esta actividad, por favor restablece tu contraseña de inmediato.
                </p>
                <p style="margin-top: 30px; color: #888888; font-size: 14px;">
                    Por seguridad, nunca compartas tu contraseña con nadie.
                </p>
                """,
                new EmailButton("Recuperar contraseña", Url("/Cuenta/Recuperar"), "#FF0000"));

        public string ActivarAcceso(string rol) =>
            EmailLayout.Render(
                "¡Bienvenido!",
                $"""
                <p style="margin-bottom: 20px; text-align: center;">
                    Su cuenta ha sido activada exitosamente como <strong>{rol}</strong>, ya puedes hacer uso de la plataforma.
                </p>
                """,
                new EmailButton("Iniciar sesión", Url("/Cuenta/Ingresar"), "#2e7d32"),
                "#2e7d32");

        public string RestringirAcceso(string rol) =>
            EmailLayout.Render(
                $"Restringimos su acceso como {rol}",
                """
                <p style="margin-bottom: 20px;">
                    Hemos detectado actividad inusual en su cuenta y por medidas de seguridad hemos aplicado restricciones temporales.
                </p>
                <p style="margin-top: 30px; color: #888888; font-size: 14px;">
                    Si considera que esto es un error, por favor responda a este correo inmediatamente.
                </p>
                """,
                colorTitulo: "#d32f2f");

        public string VerificarCorreo(string userId, string correo, string encodedToken) =>
            EmailLayout.Render(
                "Solicitud de actualización de correo",
                $"""
                <p>
                    Hemos recibido una solicitud para cambiar el correo asociado a tu cuenta del
                    <strong>Portal COSIE</strong> a este correo (<strong>{correo}</strong>).
                </p>
                <p>
                    Si tú realizaste esta solicitud, por favor confirma el cambio dando clic en el siguiente botón:
                </p>
                <p style="color: #777; font-size: 14px;">
                    Si no solicitaste este cambio, puedes ignorar este mensaje.
                    Tu cuenta seguirá vinculada a tu correo actual.
                </p>
                """,
                new EmailButton("Confirmar cambio", Url($"/Cuenta/ActualizarCorreo?id={userId}&correo={correo}&token={encodedToken}")));

        public string CorreoActualizado(string nuevoCorreo) =>
            EmailLayout.Render(
                "Tu correo ha sido actualizado",
                $"""
                <p>Queremos informarte que el correo asociado a tu cuenta ha sido actualizado correctamente.</p>
                <p style="margin: 20px 0; font-weight: bold; color: #1d72b8;">
                    Nuevo correo registrado: <span style="color: #333333;">{nuevoCorreo}</span>
                </p>
                <p>Si tú realizaste esta acción, no necesitas hacer nada más.</p>
                <p style="margin-top: 25px;">
                    <strong>¿No reconoces este cambio?</strong><br>
                    Por favor contacta inmediatamente a gestión escolar.
                </p>
                """);

        public string CelularActualizado(string nuevoCelular) =>
            EmailLayout.Render(
                "Tu número de celular ha sido actualizado",
                $"""
                <p style="margin-bottom: 20px;">
                    Te informamos que el número de celular asociado a tu cuenta ha sido actualizado correctamente.
                    El nuevo número registrado es el siguiente:
                </p>
                <p style="text-align: center; font-size: 20px; font-weight: bold; margin: 30px 0;">{nuevoCelular}</p>
                <p style="margin-top: 20px;">
                    Si realizaste este cambio, no necesitas hacer nada más.
                    Si no reconoces esta actividad, te recomendamos contactar a gestión escolar.
                </p>
                """);

        public string ContrasenaCambiada() =>
            EmailLayout.Render(
                "Tu contraseña ha sido cambiada exitosamente",
                """
                <p style="margin-bottom: 20px;">
                    Te informamos que la contraseña de tu cuenta ha sido cambiada exitosamente.
                    Si fuiste tú quien realizó este cambio, no necesitas hacer nada más.
                </p>
                <p style="margin-top: 20px;">
                    Si no reconoces esta actividad, te recomendamos restablecer tu contraseña de inmediato o contactar a gestión escolar.
                </p>
                """);

        public string EstadoTramiteCambiado(int tramiteId, string estadoNombre, string nombreAlumno, string? observaciones = null)
        {
            var observacionesHtml = string.IsNullOrWhiteSpace(observaciones)
                ? string.Empty
                : $"""
                <p style="margin: 20px 0; padding: 15px; background-color: #f5f5f5; border-radius: 6px;">
                    <strong>Observaciones:</strong><br>{observaciones}
                </p>
                """;

            return EmailLayout.Render(
                "Actualización de trámite",
                $"""
                <p>Hola <strong>{nombreAlumno}</strong>,</p>
                <p>
                    Tu trámite <strong>#{tramiteId}</strong> ha cambiado de estado a
                    <strong style="color: #1d72b8;">{estadoNombre}</strong>.
                </p>
                {observacionesHtml}
                """,
                new EmailButton("Ver seguimiento", Url($"/Tramite/SeguimientoCTCE?tramiteId={tramiteId}")));
        }
    }
}