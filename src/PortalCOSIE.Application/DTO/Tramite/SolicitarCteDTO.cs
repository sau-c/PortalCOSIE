using PortalCOSIE.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PortalCOSIE.Application.DTO.Tramite
{
    public class SolicitarCteDTO
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        public ICollection<UnidadAprendizaje> UnidadesAprendizaje { get; set; }
    }
}
