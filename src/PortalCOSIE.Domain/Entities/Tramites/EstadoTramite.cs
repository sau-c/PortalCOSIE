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
        /// Determina si el estado es final (no permite más transiciones)
        /// </summary>
        /// <returns></returns>
        public bool EsFinal()
            => this == Concluido || this == Cancelado;

        /// <summary>
        /// Valida si el estado actual puede transicionar al nuevo estado.
        /// </summary>
        public bool PuedeTransicionarA(EstadoTramite nuevoEstado)
        {
            if (nuevoEstado == null) return false; // Evitar nulls
            if (this == nuevoEstado) return false; // No se permite transicionar al mismo estado
            if (EsFinal()) return false;           // No se permite transicionar desde un estado final

            return this switch
            {
                _ when this == Solicitado =>
                    nuevoEstado == EnRevision,

                _ when this == EnRevision =>
                    nuevoEstado == DocumentosPendientes
                    || nuevoEstado == Concluido
                    || nuevoEstado == Cancelado,

                _ when this == DocumentosPendientes =>
                    nuevoEstado == EnRevision,

                _ => false
            };
        }
    }

}