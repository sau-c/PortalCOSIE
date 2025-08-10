using System.ComponentModel.DataAnnotations;

namespace PortalCOSIE.Application.DTO.Cuenta
{
    public class IngresarDTO
    {
        [Required(ErrorMessage = "Campo obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato incorrecto.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }
    }
}
