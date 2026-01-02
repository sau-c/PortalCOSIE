namespace PortalCOSIE.Application.Features.Usuarios.DTO
{
    public class RestablecerDTO
    {
        public string Token { get; set; }
        public string Correo { get; set; }
        public string NuevaContrasena { get; set; }
        public string ConfirmarContrasena { get; set; }
    }
}
