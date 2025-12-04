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
        public static readonly EstadoDocumento EnRevision = new(1, "En Revisión");

        /// <summary>Estado cuando el documento cumple con todos los requisitos establecidos</summary>
        public static readonly EstadoDocumento Validado = new(2, "Validado");

        /// <summary>Estado cuando el documento presenta problemas corregibles (firmas, sellos, fechas)</summary>
        public static readonly EstadoDocumento ConErrores = new(3, "Con errores");

        /// <summary>Estado cuando el documento no es el solicitado</summary>
        public static readonly EstadoDocumento Incorrecto = new(4, "Documento incorrecto");

        private EstadoDocumento(int id, string nombre)
            : base(id, nombre) { }

        /// <summary>
        /// Determina si el documento puede ser reemplazado o editado.
        /// </summary>
        public bool PermiteEdicion() =>
            this == ConErrores || this == Incorrecto;
    }
}