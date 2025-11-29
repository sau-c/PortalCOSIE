namespace PortalCOSIE.Domain.Entities.Tramites
{
    public class DetalleCTCE : Tramite
    {
        public string Situacion { get; private set; }
        public bool TieneDictamenesAnteriores { get; private set; }

        // Privada mutable y una expuesta como solo lectura
        private readonly List<UnidadReprobada> _unidadesReprobadas = new();
        public IReadOnlyCollection<UnidadReprobada> UnidadesReprobadas => _unidadesReprobadas.AsReadOnly();

        private DetalleCTCE() { }

        public DetalleCTCE(int alumnoId, int tipoId, string situacion, bool tieneDictamenesAnteriores, List<UnidadReprobada> unidadesReprobadas)
            : base(alumnoId, tipoId)
        {
            SetSituacion(situacion);
            TieneDictamenesAnteriores = tieneDictamenesAnteriores;

            if (unidadesReprobadas != null)
                _unidadesReprobadas.AddRange(unidadesReprobadas);
        }

        public void SetSituacion(string situacion)
        {
            if (string.IsNullOrWhiteSpace(situacion))
                throw new DomainException("El motivo de solicitud no puede estar vacío.");
            if(situacion.Length > 1000)
                throw new DomainException("El motivo de solicitud no puede exceder los 1000 caracteres.");
            Situacion = situacion;
        }
        public void AgregarUnidadReprobada(UnidadReprobada unidad)
        {
            if (unidad == null)
                throw new ArgumentNullException(nameof(unidad));

            _unidadesReprobadas.Add(unidad);
        }
    }
}