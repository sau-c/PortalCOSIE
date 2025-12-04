namespace PortalCOSIE.Domain.Entities.Calendario
{
    /// <summary>
    /// Representa una sesión programada de la Comisión de Situación Escolar (COSIE).
    /// </summary>
    /// <remarks>
    /// Cada sesión define el período de recepción de trámites y la fecha de reunión.
    /// Los trámites deben ser recibidos en las fechas establecidas para ser incluidos en la sesión.
    /// </remarks>
    public class SesionCOSIE : BaseEntity<int>
    {
        /// <summary>Identificador único de la sesión</summary>
        public string NumeroSesion { get; private set; }

        private readonly List<FechaRecepcion> _fechasRecepcion = new();

        /// <summary>Fecha programada para la reunión</summary>
        public DateTime? FechaSesion { get; private set; }

        /// <summary>Colección de fechas límite para recepción de trámites</summary>
        public IReadOnlyCollection<FechaRecepcion> FechasRecepcion => _fechasRecepcion.AsReadOnly();

        /// <summary>Constructor protegido para migraciones</summary>
        protected SesionCOSIE() { }
        
        public SesionCOSIE(string orden, DateTime fechaSesion)
        {
            SetNumeroSesion(orden);
            SetFechaSesion(fechaSesion);
        }

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

        /// <summary>
        /// Define las fechas límite para recepción de trámites
        /// </summary>
        /// <exception cref="DomainException">
        /// Cuando alguna fecha de recepción es posterior a la fecha de sesión
        /// </exception>
        /// <remarks>
        /// Las fechas se ordenan automáticamente y se eliminan duplicados.
        /// Cada fecha representa un día hábil en que se pueden recibir trámites.
        /// </remarks>
        public void SetFechasRecepcion(List<DateTime> fechasRecepcion)
        {
            if (FechaSesion.HasValue)
            {
                foreach (var fecha in fechasRecepcion)
                {
                    if (fecha >= FechaSesion.Value)
                        throw new DomainException($"La fecha de recepción {fecha:dd/MM/yyyy} debe ser anterior a la fecha de sesión");
                }
            }

            _fechasRecepcion.Clear();

            foreach (var fecha in fechasRecepcion.Distinct().OrderBy(f => f))
            {
                var fechaRecepcion = new FechaRecepcion(fecha);
                _fechasRecepcion.Add(fechaRecepcion);
            }
        }
    }
}