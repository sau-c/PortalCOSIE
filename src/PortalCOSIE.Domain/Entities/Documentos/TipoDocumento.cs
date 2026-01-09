namespace PortalCOSIE.Domain.Entities.Documentos
{
    /// <summary>
    /// Define los tipos de documento disponibles en el sistema.
    /// Implementa el patrón Enumeration para tipos seguros y extensibles.
    /// </summary>
    public class TipoDocumento : Enumeration
    {
        public static readonly TipoDocumento Identificacion = new(1, "Identificación");
        public static readonly TipoDocumento BoletaGlobal = new (2, "Boleta global");
        public static readonly TipoDocumento CartaExposicionMotivos = new (3, "Carta exposición de motivos");
        public static readonly TipoDocumento Probatorios = new (4, "Probatorios");
        public static readonly TipoDocumento DictamenCTCE = new(5, "Dictamen CTCE");

        private TipoDocumento(int id, string nombre) : base(id, nombre)
        { }
    }
}