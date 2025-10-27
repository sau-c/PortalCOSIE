namespace PortalCOSIE.Domain.Entities
{
    public class SesionCOSIE : BaseEntity
    {
        public string NumeroSesion { get; private set; }
        private readonly List<FechaRecepcion> _fechasRecepcion = new();
        public DateTime? FechaSesion { get; private set; }

        // Navegación EF CORE
        public virtual IReadOnlyCollection<FechaRecepcion> FechasRecepcion => _fechasRecepcion.AsReadOnly();

        // Constructor requerido por EF Core
        protected SesionCOSIE() { }

        // Constructor principal para crear nuevas sesiones
        public SesionCOSIE(string orden)
        {
            SetOrden(orden);
        }

        // Métodos para modificar el estado (Domain-Driven Design)
        public void SetOrden(string orden)
        {
            if (string.IsNullOrWhiteSpace(orden))
                throw new ArgumentException("La orden no puede estar vacía", nameof(orden));
            NumeroSesion = orden.Trim();
        }

        public void SetFechaSesion(DateTime fechaSesion)
        {
            if (fechaSesion == DateTime.MinValue || fechaSesion == DateTime.MaxValue)
                throw new ArgumentException("La fecha de sesión no es válida", nameof(fechaSesion));
            FechaSesion = fechaSesion;
        }

        // Métodos para manejar la colección de FechaRecepcion
        public void AgregarFechaRecepcion(FechaRecepcion fechaRecepcion)
        {
            if (fechaRecepcion == null)
                throw new ArgumentNullException(nameof(fechaRecepcion));
            _fechasRecepcion.Add(fechaRecepcion);
        }

        public void RemoverFechaRecepcion(FechaRecepcion fechaRecepcion)
        {
            if (fechaRecepcion == null)
                throw new ArgumentNullException(nameof(fechaRecepcion));
            _fechasRecepcion.Remove(fechaRecepcion);
        }

        public void LimpiarFechasRecepcion()
        {
            _fechasRecepcion.Clear();
        }

        // Método para validar el estado de la entidad
        public bool EsValida()
        {
            return !string.IsNullOrWhiteSpace(NumeroSesion) &&
                   FechaSesion != DateTime.MinValue &&
                   FechaSesion != DateTime.MaxValue;
        }
    }
}