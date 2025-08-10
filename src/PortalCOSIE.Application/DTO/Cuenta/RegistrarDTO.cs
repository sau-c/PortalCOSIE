using System.ComponentModel.DataAnnotations;

namespace PortalCOSIE.Application.DTO.Cuenta
{
    public class RegistrarDTO
    {   
        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "Solo se permiten letras.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "Solo se permiten letras.")]
        public string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "Solo se permiten letras.")]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(10, ErrorMessage = "Máximo 10 numeros")]
        public string NumeroBoleta { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public int CarreraId { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public int PlanEstudioId { get; set; }
    }
}