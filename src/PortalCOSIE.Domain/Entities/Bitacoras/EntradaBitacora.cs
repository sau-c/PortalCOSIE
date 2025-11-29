namespace PortalCOSIE.Domain.Entities.Bitacoras
{
    public class EntradaBitacora : BaseEntity
    {
        public string Accion { get; private set; }
        public string Entidad { get; private set; }
        public string EntidadId { get; private set; }
        public string? ValorNuevo { get; private set; }
        public string? IdentityUserId { get; private set; }        
        public string? IpAddress { get; private set; }
        public string? UserAgent { get; private set; }
        public DateTime FechaRegistro { get; private set; }

        private EntradaBitacora() { }

        //Para crear registros
        public EntradaBitacora(string accion, string entidad, string entidadId, string valorNuevo, string identityUserId, string ipAddress, string userAgent)
        {
            Accion = accion;
            Entidad = entidad;
            EntidadId = entidadId;
            ValorNuevo = valorNuevo;
            IdentityUserId = identityUserId;
            IpAddress = ipAddress;
            UserAgent = userAgent;
            FechaRegistro = DateTime.Now;
        }


        public EntradaBitacora(int id, string accion, string entidad, string entidadId, string valorNuevo,
            string identityUserId, string ipAddress, string userAgent, DateTime fechaRegistro)
        {
            Id = id;
            Accion = accion;
            Entidad = entidad;
            EntidadId = entidadId;
            ValorNuevo = valorNuevo;
            IdentityUserId = identityUserId;
            IpAddress = ipAddress;
            UserAgent = userAgent;
            FechaRegistro = fechaRegistro;
        }
    }
}