using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Application.DTO.Usuario
{
    public class AlumnoConIdentityDTO
    {
        public string IdentityUserId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string PeriodoIngreso { get; set; }
        public string NumeroBoleta { get; set; }
        public Carrera Carrera { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string Rol { get; set; }
    }
}
