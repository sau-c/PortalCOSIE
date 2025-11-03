namespace PortalCOSIE.Domain.Entities.Calendario
{
    public class SesionCOSIE : BaseEntity
    {
        public string NumeroSesion { get; private set; }
        private readonly List<FechaRecepcion> _fechasRecepcion = new();
        public DateTime? FechaSesion { get; private set; }

        // Navegación EF CORE
        public IReadOnlyCollection<FechaRecepcion> FechasRecepcion => _fechasRecepcion.AsReadOnly();

        // Constructor requerido por EF Core
        protected SesionCOSIE() { }

        // Constructor principal para crear nuevas sesiones
        public SesionCOSIE(string orden, DateTime fechaSesion)
        {
            SetNumeroSesion(orden);
            SetFechaSesion(fechaSesion);
            //SetFechasRecepcion(fechasRecepcion);
        }

        // Métodos para modificar el estado (Domain-Driven Design)
        public void SetNumeroSesion(string orden)
        {
            if (string.IsNullOrWhiteSpace(orden))
                throw new DomainException("El numero de sesion no puede estar vacío");
            NumeroSesion = orden.Trim();
        }

        public void SetFechaSesion(DateTime fechaSesion)
        {
            if (fechaSesion == DateTime.MinValue || fechaSesion == DateTime.MaxValue)
                throw new DomainException("La fecha de sesión no es válida");
            FechaSesion = fechaSesion;
        }

        // Métodos para manejar la colección de FechaRecepcion
        public void SetFechasRecepcion(List<DateTime> fechasRecepcion)
        {
            // Validar que las fechas sean anteriores a la fecha de sesión
            if (FechaSesion.HasValue)
            {
                foreach (var fecha in fechasRecepcion)
                {
                    if (fecha >= FechaSesion.Value)
                        throw new DomainException($"La fecha de recepción {fecha:dd/MM/yyyy} debe ser anterior a la fecha de sesión");
                }
            }

            // Limpiar fechas existentes si las hay
            _fechasRecepcion.Clear();

            // Crear y agregar las nuevas fechas
            foreach (var fecha in fechasRecepcion.Distinct().OrderBy(f => f))
            {
                var fechaRecepcion = new FechaRecepcion(fecha);
                _fechasRecepcion.Add(fechaRecepcion);
            }
        }

        public void RemoverFechasRecepcion(FechaRecepcion fechaRecepcion)
        {
            if (fechaRecepcion == null)
                throw new DomainException("No se enviaron fechas a remover");
            _fechasRecepcion.Remove(fechaRecepcion);
        }
    }
}