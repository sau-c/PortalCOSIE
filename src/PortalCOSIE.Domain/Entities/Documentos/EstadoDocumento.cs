using PortalCOSIE.Domain.Common;

namespace PortalCOSIE.Domain.Entities.Documentos
{
    public class EstadoDocumento : Enumeration
    {
        public static readonly EstadoDocumento EnRevision = new(1, "En Revisión");
        public static readonly EstadoDocumento Validado = new(2, "Validado");
        public static readonly EstadoDocumento ConErrores = new(3, "Con errores");
        public static readonly EstadoDocumento Incorrecto = new(4, "Documento incorrecto");

        private EstadoDocumento(int id, string nombre)
            : base(id, nombre) { }

        // Lógica adicional opcional
        public bool PermiteEdicion() =>
            this == ConErrores || this == Incorrecto;
    }
}
