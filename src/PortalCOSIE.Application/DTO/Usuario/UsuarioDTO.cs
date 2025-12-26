namespace PortalCOSIE.Application.DTO.Usuario
{
    public class UsuarioDTO
    {
        public string IdentityUserId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public bool CorreoConfirmado { get; set; }
        public string Rol { get; set; }
    }
}
