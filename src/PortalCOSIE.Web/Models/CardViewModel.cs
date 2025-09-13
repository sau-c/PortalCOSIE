using Microsoft.AspNetCore.Html;

namespace PortalCOSIE.Web.Models
{
    public class CardViewModel
    {
        public string HeaderTitle { get; set; }
        public bool ShowButton { get; set; } = false;
        public string ButtonAction { get; set; }
        public string ButtonText { get; set; }
        public IHtmlContent BodyContent { get; set; } // Permite contenido HTML dinámico
    }
}
