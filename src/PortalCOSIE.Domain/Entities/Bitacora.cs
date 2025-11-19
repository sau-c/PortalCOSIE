namespace PortalCOSIE.Domain.Entities
{
    public class Bitacora : BaseEntity
    {
        public string Accion { get; private set; }
        public string Entidad { get; private set; }
        public string EntidadId { get; private set; }
        public string IdentityUserId { get; private set; }        
        public string IpAddress { get; private set; }
        public string UserAgent { get; private set; }
        public DateTime FechaRegistro { get; private set; }

        private Bitacora() { } // Para EF

        public Bitacora(string identityUserId, string accion, string entidad, string entidadId, string ipAddress, string userAgent)
        {
            IdentityUserId = identityUserId;
            Accion = accion;
            Entidad = entidad;
            EntidadId = entidadId;
            IpAddress = ipAddress;
            UserAgent = userAgent;
            FechaRegistro = DateTime.UtcNow;
        }
    }
}