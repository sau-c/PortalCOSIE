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
        /// <summary> Estado cuando el tramite esperando </summary>
        public static readonly EstadoTramite EsperandoAcuse = new(6, "Esperando acuse");

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
            if (nuevoEstado == null) return false;
            if (EsFinal()) return false;
            if (this.Id == nuevoEstado.Id) return true;

            return this.Id switch
            {
                // Solicitado
                1 => nuevoEstado.Id == 2,
                // EnRevision
                2 => nuevoEstado.Id == 3 || nuevoEstado.Id == 5 || nuevoEstado.Id == 6,
                // DocumentosPendientes
                3 => nuevoEstado.Id == 2,
                6 => nuevoEstado.Id == 4,
                _ => false
            };
        }
        
    }

}