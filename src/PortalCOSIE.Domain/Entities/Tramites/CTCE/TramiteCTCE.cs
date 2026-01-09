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
            EstablecerPeticion(peticion);
            TieneDictamenesAnteriores = tieneDictamenesAnteriores;
            if (unidadesReprobadas != null)
                _unidadesReprobadas.AddRange(unidadesReprobadas);
            AgregarDocumento(identificacion);
            AgregarDocumento(cartaMotivos);
            AgregarDocumento(boletaGlobal);
            AgregarDocumento(probatorios);
        }
        private void EstablecerPeticion(string peticion)
        {
            if (string.IsNullOrWhiteSpace(peticion))
                throw new DomainException("El motivo de solicitud no puede estar vacío.");
            if (peticion.Length > 1000)
                throw new DomainException("El motivo de solicitud no puede exceder los 1000 caracteres.");
            Peticion = peticion;
        }

        public void VerificarEstadoTramite()
        {
            if (Documentos.Any(d => d.EstadoDocumentoId == EstadoDocumento.EnRevision.Id))
            {
                CambiarEstado(EstadoTramite.EnRevision);
                return;
            }

            //if (Documentos.Any(d => d.EstadoDocumentoId == EstadoDocumento.Incorrecto.Id || d.EstadoDocumentoId == EstadoDocumento.ConErrores.Id))
            if (Documentos.Any(d => d.PermiteCorreccion()))
            {
                CambiarEstado(EstadoTramite.DocumentosPendientes);
                return;
            }
        }
    }
}