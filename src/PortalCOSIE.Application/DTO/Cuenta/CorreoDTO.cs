using System.ComponentModel.DataAnnotations;

namespace PortalCOSIE.Application.DTO.Cuenta
{
    public class CorreoDTO
    {
        [Required(ErrorMessage = "Campo obligatorio.")]
        [EmailAddress]
        public string Correo { get; set; }
    }
}
