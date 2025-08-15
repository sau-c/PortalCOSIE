using System.ComponentModel.DataAnnotations;

namespace PortalCOSIE.Application.DTO.Usuario
{
    public class AlumnoDTO
    {
        public string IdentityUserId { get; set; }
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
        [DataType(DataType.Date, ErrorMessage = "Formato de fecha inválido")]
        public DateTime FechaIngreso { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(10, ErrorMessage = "Máximo 10 numeros")]
        public string NumeroBoleta { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public int? CarreraId { get; set; }
        public string? CarreraNombre { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public int? PlanEstudioId { get; set; }
        public string? PlanEstudioNombre { get; set; }
    }
}
