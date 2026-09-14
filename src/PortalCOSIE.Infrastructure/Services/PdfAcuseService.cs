using System.Text;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using PortalCOSIE.Application.Features.Tramites.DTO;
using PortalCOSIE.Application.Services.Crypto;
using QRCoder;

namespace PortalCOSIE.Infrastructure.Services;

public class PdfAcuseService : IPdfAcuseService
{
    private const float QrSize = 80f;
    private const float Margin = 12f;

    public byte[] GenerarPanelAcusePublico(
        byte[] pdfFirmado,
        AcusePdfMetadatosDTO metadatos,
        string firmaCmsBase64)
    {
        if (pdfFirmado.Length == 0)
            throw new InvalidOperationException("El PDF del acuse no puede estar vacío.");
        if (metadatos == null)
            throw new InvalidOperationException("Los metadatos del acuse son obligatorios.");
        if (string.IsNullOrWhiteSpace(firmaCmsBase64))
            throw new InvalidOperationException("La firma en Base64 es obligatoria.");

        using var input = new MemoryStream(pdfFirmado);
        using var output = new MemoryStream();
        using var reader = new PdfReader(input);
        using var writer = new PdfWriter(output);
        using var pdfDoc = new PdfDocument(reader, writer);

        var page = pdfDoc.GetLastPage();
        var pageSize = page.GetPageSize();
        var pageNumber = pdfDoc.GetNumberOfPages();
        var panelWidth = pageSize.GetWidth() - (2 * Margin);
        var textoWidth = panelWidth - QrSize - 10f;

        var firmante = ExtraerCn(metadatos.FirmanteSujeto);
        var emisor = ExtraerCn(metadatos.EmisorCaSujeto);
        var fuenteMonoespaciada = PdfFontFactory.CreateFont(StandardFonts.COURIER);

        var panel = new Table(UnitValue.CreatePercentArray(new float[] { textoWidth, QrSize }))
            .SetWidth(panelWidth)
            .SetFixedPosition(pageNumber, Margin, Margin, panelWidth)
            .SetBackgroundColor(new DeviceRgb(248, 249, 250))
            .SetBorder(new SolidBorder(new DeviceRgb(108, 117, 125), 0.5f));

        var contenido = new Div()
            .SetPadding(4)
            .Add(new Paragraph(ConstruirTextoCompacto(metadatos, firmante, emisor))
                .SetFontSize(5.5f)
                .SetMarginBottom(1)
                .SetMultipliedLeading(1.08f))
            .Add(new Paragraph(FormatearBase64EnLineas(firmaCmsBase64.Trim(), 149))
                .SetFont(fuenteMonoespaciada)
                .SetFontSize(5.5f)
                .SetMultipliedLeading(1.05f));

        var celdaTexto = new Cell().Add(contenido).SetBorder(Border.NO_BORDER);
        var celdaQr = new Cell()
            .SetBorder(Border.NO_BORDER)
            .SetVerticalAlignment(VerticalAlignment.MIDDLE)
            .SetTextAlignment(TextAlignment.CENTER);

        var qrBytes = GenerarQrPng(metadatos.UrlVerificacion.Trim());
        celdaQr.Add(new Image(ImageDataFactory.Create(qrBytes))
            .SetWidth(QrSize - 6)
            .SetHeight(QrSize - 6));

        panel.AddCell(celdaTexto);
        panel.AddCell(celdaQr);

        using (var canvas = new Canvas(new PdfCanvas(page), pageSize))
        {
            canvas.Add(panel);
        }

        pdfDoc.Close();
        return output.ToArray();
    }

    private static string ConstruirTextoCompacto(
        AcusePdfMetadatosDTO metadatos,
        string firmante,
        string emisor)
    {
        var sb = new StringBuilder();
        sb.Append("FIRMA ELECTRÓNICA | ");
        sb.Append(firmante);
        sb.Append(" | CA: ");
        sb.Append(emisor);
        sb.Append(" | Serie: ");
        sb.AppendLine(metadatos.NumeroSerie);
        sb.Append("Trámite #");
        sb.Append(metadatos.TramiteId);
        sb.Append(" | ");
        sb.Append(metadatos.AlumnoNombre);
        sb.Append(" (");
        sb.Append(metadatos.NumeroBoleta);
        sb.Append(") | ");
        sb.Append(metadatos.FechaFirmaLocal.ToString("dd/MM/yyyy HH:mm"));
        sb.Append(" | Vig. ");
        sb.Append(metadatos.VigenteDesde.ToString("dd/MM/yyyy"));
        sb.Append('-');
        sb.Append(metadatos.VigenteHasta.ToString("dd/MM/yyyy"));
        return sb.ToString();
    }

    private static string FormatearBase64EnLineas(string base64, int caracteresPorLinea)
    {
        if (base64.Length <= caracteresPorLinea)
            return base64;

        var sb = new StringBuilder();
        for (var i = 0; i < base64.Length; i += caracteresPorLinea)
        {
            var longitud = Math.Min(caracteresPorLinea, base64.Length - i);
            sb.AppendLine(base64.Substring(i, longitud));
        }

        return sb.ToString().TrimEnd();
    }

    private static string ExtraerCn(string sujetoDn)
    {
        if (string.IsNullOrWhiteSpace(sujetoDn))
            return "Sin nombre";

        foreach (var parte in sujetoDn.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            if (parte.StartsWith("CN=", StringComparison.OrdinalIgnoreCase))
                return parte[3..].Trim();
        }

        return sujetoDn.Trim();
    }

    private static byte[] GenerarQrPng(string contenido)
    {
        using var generator = new QRCodeGenerator();
        var data = generator.CreateQrCode(contenido, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new PngByteQRCode(data);
        return qrCode.GetGraphic(8);
    }
}