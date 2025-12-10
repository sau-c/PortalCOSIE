using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace PortalCOSIE.Web.TagHelpers
{
    [HtmlTargetElement("chart-card")]
    public class ChartCardTagHelper : TagHelper
    {
        public string ApiUrl { get; set; }
        public string ChartType { get; set; } = "doughnut"; // Default        
        public string Title { get; set; }
        public string ChartId { get; set; }
        public string LoaderId { get; set; }

        // Filtro 1
        [HtmlAttributeName("filtro1-items")]
        public IEnumerable<SelectListItem>? Filtro1Items { get; set; }
        public string? Filtro1Name { get; set; } = "filtro1"; // Valor por defecto seguro

        // Filtro 2
        [HtmlAttributeName("filtro2-items")]
        public IEnumerable<SelectListItem>? Filtro2Items { get; set; }
        public string? Filtro2Name { get; set; } = "filtro2";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            // Agregamos data-attributes para que el JS los lea
            output.Attributes.SetAttribute("class", "chart-card card shadow h-100");
            output.Attributes.SetAttribute("data-component", "chart-widget"); // Marcador para inicializar
            output.Attributes.SetAttribute("data-api-url", ApiUrl);
            output.Attributes.SetAttribute("data-chart-type", ChartType);
            output.Attributes.SetAttribute("data-chart-id", ChartId);
            output.Attributes.SetAttribute("data-loader-id", LoaderId);

            // Construcción de filtros dinámicos (sin IDs fijos necesarios para el JS, usaremos selectores relativos)
            string filtrosHtml = "";

            if (Filtro1Items != null)
            {
                filtrosHtml += BuildSelect(Filtro1Name, Filtro1Items);
            }

            if (Filtro2Items != null)
            {
                filtrosHtml += BuildSelect(Filtro2Name, Filtro2Items);
            }

            output.Content.SetHtmlContent($@"
                <div class='card-header d-flex justify-content-between align-items-center flex-wrap'>
                    <h6 class='mb-0 py-1'>{Title}</h6>
                    <div class='d-flex filters-container'>
                        {filtrosHtml}
                    </div>
                </div>
                <div class='card-body'>
                    <div class='chart-container'>
                        <canvas id='{ChartId}'></canvas>
                    </div>
                    <div id='{LoaderId}' class='chart-loader text-center d-none'
                         style='position:absolute; top:50%; left:50%; transform:translate(-50%, -50%)'>
                         <div class='spinner-border text-primary' role='status'></div>
                    </div>
                </div>
            ");
        }
        private string BuildSelect(string name, IEnumerable<SelectListItem> items)
        {
            // Inputs estándar de Bootstrap (form-select-sm)
            var options = string.Join("", items.Select(i => $"<option value='{i.Value}'>{i.Text}</option>"));
            return $"<select name='{name}' class='chart-filter form-select form-select-sm w-auto ms-2'>{options}</select>";
        }
    }
}