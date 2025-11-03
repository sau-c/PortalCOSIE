namespace PortalCOSIE.Domain.Entities.Calendario
{
    public class FechaRecepcion : BaseEntity
    {
        public int SesionId { get; private set; }
        public DateTime Fecha { get; private set; }

        // Navegación EF CORE
        public SesionCOSIE Sesion { get; private set; }

        protected FechaRecepcion() { }

        public FechaRecepcion(DateTime fecha)
        {
            Fecha = fecha;
        }
    }
}