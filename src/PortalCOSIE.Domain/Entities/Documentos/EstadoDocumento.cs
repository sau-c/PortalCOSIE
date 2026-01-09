namespace PortalCOSIE.Domain.Entities.Documentos
{
    /// <summary>
    /// Define los estados de validación de documentos en el proceso de revisión de trámites.
    /// </summary>
    /// <remarks>
    /// El ciclo de vida de un documento pasa por estos estados para garantizar 
    /// cumplimiento antes de su aprobación final.
    /// </remarks>
    public class EstadoDocumento : Enumeration
    {
        /// <summary>Estado inicial cuando el documento ha sido cargado y está pendiente de revisión</summary>
        public static readonly EstadoDocumento EnRevision = new(1, "En revisión");
        /// <summary>Estado cuando el documento cumple con todos los requisitos establecidos</summary>
        public static readonly EstadoDocumento Validado = new(2, "Validado");
        /// <summary>Estado cuando el documento presenta problemas corregibles (firmas, sellos, fechas)</summary>
        public static readonly EstadoDocumento ConErrores = new(3, "Con errores");
        /// <summary>Estado cuando el documento no es el solicitado</summary>
        public static readonly EstadoDocumento Incorrecto = new(4, "Documento incorrecto");

        private EstadoDocumento(int id, string nombre)
            : base(id, nombre) { }

        /// <summary>
        /// Determina si el estado actual es final y ya no permite edicion (Validado)
        /// </summary>
        /// <returns></returns>
        public bool EsFinal()
            => this == Validado;

        /// <summary>
        /// Valida si el estado actual puede transicionar al nuevo estado.
        /// </summary>
        public bool PuedeTransicionarA(EstadoDocumento nuevoEstado)
        {
            if (nuevoEstado == null) return false;
            if (EsFinal()) return false;
            if (this.Id == nuevoEstado.Id) return false; //No se permite la redundancia

            return this.Id switch
            {
                1 => nuevoEstado.Id == 2 || nuevoEstado.Id == 3 || nuevoEstado.Id == 4, // EnRevision
                3 => nuevoEstado.Id == 1 , // ConErrores
                4 => nuevoEstado.Id == 1, // Incorrecto
                _ => false
            };
        }

    }
}