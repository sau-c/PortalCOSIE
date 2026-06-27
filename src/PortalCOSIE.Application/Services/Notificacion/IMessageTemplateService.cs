namespace PortalCOSIE.Application.Services.Notificacion
{
    public interface IMessageTemplateService
    {
        string ConfirmarCorreo(string correo, string encodedToken);
        string RecuperarContrasena(string correo, string encodedToken);
        string ContrasenaRestablecida();
        string ActivarAcceso(string rol);
        string RestringirAcceso(string rol);
        string VerificarCorreo(string userId, string correo, string encodedToken);
        string CorreoActualizado(string nuevoCorreo);
        string CelularActualizado(string nuevoCelular);
        string ContrasenaCambiada();
        string EstadoTramiteCambiado(int tramiteId, string estadoNombre, string nombreAlumno, string? observaciones = null);
    }
}