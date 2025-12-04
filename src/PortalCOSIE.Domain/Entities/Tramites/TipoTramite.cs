namespace PortalCOSIE.Domain.Entities.Tramites
{
    /// <summary>
    /// Define los tipos de trámites disponibles en el sistema.
    /// Implementa el patrón Enumeration para tipos seguros y extensibles.
    /// </summary>
    public class TipoTramite : Enumeration
    {
        /// <summary>Dictamen interno procesado por el Consejo Técnico Consultivo Escolar (CTCE)</summary>
        public static readonly TipoTramite DictamenInterno = new(1, "Dictamen interno (CTCE)");

        /// <summary>Dictamen externo procesado por el Consejo General Consultivo (CGC)</summary>
        public static readonly TipoTramite DictamenExterno = new(2, "Dictamen externo (CGC)");

        private TipoTramite(int id, string nombre) : base(id, nombre)
        {
        }
    }
}