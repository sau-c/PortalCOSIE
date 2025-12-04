namespace PortalCOSIE.Domain.Entities.Tramites
{
    /// <summary>
    /// Representa datos específicos de un trámite del Consejo Técnico Consultivo Escolar (CTCE).
    /// </summary>
    public class DetalleCTCE : Tramite
    {
        /// <summary>Descripción de la situación académica del alumno por la cual solicita este tramite</summary>
        public string Situacion { get; private set; }

        public bool TieneDictamenesAnteriores { get; private set; }

        // Colección privada mutable con exposición de solo lectura para respetar el dominio
        private readonly List<UnidadReprobada> _unidadesReprobadas = new();

        /// <summary>Colección de unidades de aprendizaje reprobadas asociadas al caso</summary>
        public IReadOnlyCollection<UnidadReprobada> UnidadesReprobadas => _unidadesReprobadas.AsReadOnly();

        /// <summary>Constructor privado para migraciones</summary>
        private DetalleCTCE() { }

        public DetalleCTCE(
            int alumnoId,
            int tipoId,
            string periodoSolicitud,
            string situacion,
            bool tieneDictamenesAnteriores,
            List<UnidadReprobada> unidadesReprobadas
            ) 
            : base(alumnoId, tipoId, periodoSolicitud)
        {
            SetSituacion(situacion);
            TieneDictamenesAnteriores = tieneDictamenesAnteriores;

            if (unidadesReprobadas != null)
                _unidadesReprobadas.AddRange(unidadesReprobadas);
        }

        private void SetSituacion(string situacion)
        {
            if (string.IsNullOrWhiteSpace(situacion))
                throw new DomainException("El motivo de solicitud no puede estar vacío.");
            if (situacion.Length > 1000)
                throw new DomainException("El motivo de solicitud no puede exceder los 1000 caracteres.");
            Situacion = situacion;
        }
        //public void AgregarUnidadReprobada(UnidadReprobada unidad)
        //{
        //    if (unidad == null) throw new ArgumentNullException(nameof(unidad));
        //    _unidadesReprobadas.Add(unidad);
        //}
    }
}