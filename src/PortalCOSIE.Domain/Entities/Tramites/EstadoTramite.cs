namespace PortalCOSIE.Domain.Entities.Tramites
{
    /// <summary>
    /// Define los posibles estados de un trámite a lo largo de su ciclo de vida.
    /// Implementa lógica de dominio específica para validaciones de estado.
    /// </summary>
    public class EstadoTramite : Enumeration
    {
        /// <summary>Estado inicial cuando el alumno solicita el trámite</summary>
        public static readonly EstadoTramite Solicitado = new(1, "Solicitado");

        /// <summary>Estado cuando el personal asignado está revisando el trámite</summary>
        public static readonly EstadoTramite EnRevision = new(2, "En revision");

        /// <summary>Estado cuando el alumno tinene que corregir documentos</summary>
        public static readonly EstadoTramite DocumentosPendientes = new(3, "Documentos pendientes");

        /// <summary>Estado final cuando el trámite ha sido procesado</summary>
        public static readonly EstadoTramite Concluido = new(4, "Concluido");

        /// <summary>Estado cuando el trámite ha sido cancelado</summary>
        public static readonly EstadoTramite Cancelado = new(5, "Cancelado");

        private EstadoTramite(int id, string nombre) : base(id, nombre)
        { }

        /// <summary>
        /// Determina si el trámite en este estado permite ediciones.
        /// </summary>
        public bool PermiteEdicion() =>
            this == Solicitado || this == EnRevision;
    }
}