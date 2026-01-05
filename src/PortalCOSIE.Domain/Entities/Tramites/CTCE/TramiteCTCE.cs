using PortalCOSIE.Domain.Entities.Documentos;

namespace PortalCOSIE.Domain.Entities.Tramites.CTCE
{
    /// <summary>
    /// Representa datos específicos de un trámite del Consejo Técnico Consultivo Escolar (CTCE).
    /// </summary>
    public class TramiteCTCE : Tramite
    {
        /// <summary>Descripción de la petición académica del alumno por la cual solicita este tramite</summary>
        public string Peticion { get; private set; }

        public bool TieneDictamenesAnteriores { get; private set; }

        // Colección privada mutable con exposición de solo lectura para respetar el dominio
        private readonly List<UnidadReprobada> _unidadesReprobadas = new();

        /// <summary>Colección de unidades de aprendizaje reprobadas asociadas al caso</summary>
        public IReadOnlyCollection<UnidadReprobada> UnidadesReprobadas => _unidadesReprobadas.AsReadOnly();

        /// <summary>Constructor privado para migraciones</summary>
        private TramiteCTCE() { }

        /// <summary>
        /// Crear una nueva instancia de TramiteCTCE
        /// </summary>
        public TramiteCTCE(
            int alumnoId,
            int tipoId,
            string periodoSolicitud,
            string peticion,
            bool tieneDictamenesAnteriores,
            List<UnidadReprobada> unidadesReprobadas,
            Documento identificacion,
            Documento cartaMotivos,
            Documento boletaGlobal,
            Documento probatorios
            ) 
            : base(alumnoId, tipoId, periodoSolicitud)
        {
            SetPeticion(peticion);
            TieneDictamenesAnteriores = tieneDictamenesAnteriores;
            if (unidadesReprobadas != null)
                _unidadesReprobadas.AddRange(unidadesReprobadas);
            SetDocumentoObligatorio(identificacion, TipoDocumento.Identificacion);
            SetDocumentoObligatorio(cartaMotivos, TipoDocumento.CartaExposicionMotivos);
            SetDocumentoObligatorio(boletaGlobal, TipoDocumento.BoletaGlobal);
            SetDocumentoObligatorio(probatorios, TipoDocumento.Probatorios);
        }
        private void SetPeticion(string peticion)
        {
            if (string.IsNullOrWhiteSpace(peticion))
                throw new DomainException("El motivo de solicitud no puede estar vacío.");
            if (peticion.Length > 1000)
                throw new DomainException("El motivo de solicitud no puede exceder los 1000 caracteres.");
            Peticion = peticion;
        }
        private void SetDocumentoObligatorio(Documento documento, TipoDocumento tipo)
        {
            if (documento == null || documento.HashOriginal.Length == 0)
                throw new DomainException($"El documento {tipo.Nombre} es obligatorio.");

            // Usamos el método de la clase base Tramite
            AgregarDocumento(documento);
        }
    }
}