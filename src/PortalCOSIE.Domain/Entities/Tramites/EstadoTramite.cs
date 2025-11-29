using PortalCOSIE.Domain.Common;

namespace PortalCOSIE.Domain.Entities.Tramites
{
    public class EstadoTramite : Enumeration
    {
        public static readonly EstadoTramite Solicitado = new(1, "Solicitado");
        public static readonly EstadoTramite EnRevision = new(2, "En revision");
        public static readonly EstadoTramite DocumentosPendientes = new(3, "Documentos pendientes");
        public static readonly EstadoTramite Concluido = new(4, "Concluido");
        public static readonly EstadoTramite Cancelado = new(5, "Cancelado");

        private EstadoTramite(int id, string nombre) : base(id, nombre)
        {
        }

        // Ejemplo de lógica de dominio
        public bool PermiteEdicion() =>
            this == Solicitado || this == EnRevision;
    }
}
