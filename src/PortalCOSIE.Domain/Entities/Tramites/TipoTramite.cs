using PortalCOSIE.Domain.Common;

namespace PortalCOSIE.Domain.Entities.Tramites
{
    public class TipoTramite : Enumeration
    {
        public static readonly TipoTramite DictamenInterno = new(1, "Dictamen interno (CTCE)");
        public static readonly TipoTramite DictamenExterno = new(2, "Dictamen externo (CGC)");

        private TipoTramite(int id, string nombre) : base(id, nombre)
        {
        }
    }
}
