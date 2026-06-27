namespace PortalCOSIE.Infrastructure.Email
{
    internal readonly record struct EmailButton(string Texto, string Url, string Color = "#1d72b8");

    internal static class EmailLayout
    {
        public static string Render(string titulo, string contenido, EmailButton? boton = null, string colorTitulo = "#333333")
        {
            var botonHtml = boton is null ? string.Empty : $@"
                <p style=""text-align: center; margin: 30px 0;"">
                    <a href=""{boton.Value.Url}"" style=""display: inline-block; background-color: {boton.Value.Color}; color: #ffffff; padding: 14px 28px; text-decoration: none; border-radius: 6px; font-weight: bold;"">
                        {boton.Value.Texto}
                    </a>
                </p>";

            return $@"
            <body style=""margin: 0; padding: 0; font-family: 'Segoe UI', sans-serif; background-color: #f6f9fc;"">
                <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                    <tr>
                        <td align=""center"" style=""padding: 40px 0;"">
                            <table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 5px rgba(0,0,0,0.05); padding: 40px;"">
                                <tr>
                                    <td align=""center"" style=""padding-bottom: 20px;"">
                                        <h1 style=""margin: 0; font-size: 24px; color: {colorTitulo};"">{titulo}</h1>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""color: #555555; font-size: 16px; line-height: 1.5;"">
                                        {contenido}
                                        {botonHtml}
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""padding-top: 30px; text-align: center; font-size: 12px; color: #aaaaaa;"">
                                        © {DateTime.Now.Year} - Portal COSIE.
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </body>";
        }
    }
}