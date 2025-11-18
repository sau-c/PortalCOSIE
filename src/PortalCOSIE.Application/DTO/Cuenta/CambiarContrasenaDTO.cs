namespace PortalCOSIE.Application.DTO.Cuenta
{
    public class CambiarContrasenaDTO
    {
        public string IdentityUserId { get; set; }
        public string Contrasena { get; set; }
        public string NuevaContrasena { get; set; }
        public string ConfirmarContrasena { get; set; }
    }
}
