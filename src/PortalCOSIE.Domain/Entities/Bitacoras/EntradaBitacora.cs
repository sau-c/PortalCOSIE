namespace PortalCOSIE.Domain.Entities.Bitacoras
{
    /// <summary>
    /// Representa un registro de auditoría en el sistema.
    /// </summary>
    /// <remarks>
    /// Captura información detallada de acciones realizadas por usuarios,
    /// incluyendo cambios en entidades, acceso al sistema y operaciones críticas.
    /// Utilizado para trazabilidad, auditoría interna y resolución de incidentes.
    /// </remarks>
    public class EntradaBitacora : BaseEntity<int>
    {
        /// <summary>Acción realizada (ej: "Crear", "Editar", "Eliminar"</summary>
        public string Accion { get; private set; }

        /// <summary>Nombre de la entidad afectada (ej: "Tramite", "Documento", "Usuario")</summary>
        public string Entidad { get; private set; }

        /// <summary>Identificador de la instancia específica de la entidad</summary>
        public string EntidadId { get; private set; }

        /// <summary>Valores nuevos o cambios realizados (JSON o descripción)</summary>
        public string? ValorNuevo { get; private set; }

        /// <summary>Usuario de Identity que realizó la acción</summary>
        public string? IdentityUserId { get; private set; }

        /// <summary>Dirección IP desde donde se realizó la acción</summary>
        public string? IpAddress { get; private set; }

        /// <summary>Navegador/cliente utilizado</summary>
        public string? UserAgent { get; private set; }

        /// <summary>Fecha y hora del registro</summary>
        public DateTime FechaRegistro { get; private set; }

        /// <summary>Constructor privado para migraciones</summary>
        private EntradaBitacora() { }

        /// <summary>
        /// Constructor principal para crear un nuevo registro de bitácora
        /// </summary>
        public EntradaBitacora(string accion, string entidad, string entidadId, string valorNuevo,
            string identityUserId, string ipAddress, string userAgent)
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

        /// <summary>
        /// Constructor completo para reconstruir registros y desacoplar identity para obtener su Id de ususario
        /// </summary>
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