namespace PortalCOSIE.Application
{
    public static class HtmlTemplates
    {
        public static string ConfirmarCorreoHtml(string correo, string encodedToken)
        {
            return $@"
            <body style=""margin: 0; padding: 0; font-family: 'Segoe UI', sans-serif; background-color: #f6f9fc;"">
                <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                    <tr>
                    <td align=""center"" style=""padding: 40px 0;"">
                        <table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 5px rgba(0,0,0,0.05); padding: 40px;"">
                        <tr>
                            <td align=""center"" style=""padding-bottom: 20px;"">
                            <h1 style=""margin: 0; font-size: 24px; color: #333333;"">Confirma tu cuenta</h1>
                            </td>
                        </tr>
                        <tr>
                            <td style=""color: #555555; font-size: 16px; line-height: 1.5;"">
                            <p style=""margin-bottom: 20px;"">
                                Para completar tu registro, por favor confirma tu correo electrónico haciendo clic en el siguiente botón:
                            </p>
                            <p style=""text-align: center; margin: 30px 0;"">
                                <a href=""https://localhost:7276/Cuenta/Confirmar?correo={correo}&token={encodedToken}"" style=""display: inline-block; background-color: #1d72b8; color: #ffffff; padding: 14px 28px; text-decoration: none; border-radius: 6px; font-weight: bold;"">
                                Confirmar cuenta
                                </a>
                            </p>
                            <p style=""margin-top: 30px; color: #888888; font-size: 14px;"">
                                Si no creaste una cuenta, puedes ignorar este correo.
                            </p>
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

        public static string RecuperarContrasenaHtml(string correo, string encodedToken)
        {
            return $@"
            <body style=""margin: 0; padding: 0; font-family: 'Segoe UI', sans-serif; background-color: #f6f9fc;"">
                <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                <tr>
                    <td align=""center"" style=""padding: 40px 0;"">
                    <table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 5px rgba(0,0,0,0.05); padding: 40px;"">
                        <tr>
                        <td align=""center"" style=""padding-bottom: 20px;"">
                            <h1 style=""margin: 0; font-size: 24px; color: #333333;"">Restablece tu contraseña</h1>
                        </td>
                        </tr>
                        <tr>
                        <td style=""color: #555555; font-size: 16px; line-height: 1.5;"">
                            <p style=""margin-bottom: 20px;"">
                            Hemos recibido una solicitud para restablecer tu contraseña. Si realizaste esta solicitud, haz clic en el botón de abajo para continuar:
                            </p>
                            <p style=""text-align: center; margin: 30px 0;"">
                            <a href=""https://localhost:7276/Cuenta/Restablecer?correo={correo}&token={encodedToken}"" style=""display: inline-block; background-color: #1d72b8; color: #ffffff; padding: 14px 28px; text-decoration: none; border-radius: 6px; font-weight: bold;"">
                                Restablecer contraseña
                            </a>
                            </p>
                            <p style=""margin-top: 30px; color: #888888; font-size: 14px;"">
                            Si no solicitaste este cambio, puedes ignorar este correo. Tu contraseña actual permanecerá sin cambios.
                            </p>
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

        public static string ContrasenaRestablecidaHtml()
        {
            return $@"
            <body style=""margin: 0; padding: 0; font-family: 'Segoe UI', sans-serif; background-color: #f6f9fc;"">
                <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                <tr>
                    <td align=""center"" style=""padding: 40px 0;"">
                    <table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 5px rgba(0,0,0,0.05); padding: 40px;"">
                        <tr>
                        <td align=""center"" style=""padding-bottom: 20px;"">
                            <h1 style=""margin: 0; font-size: 24px; color: #333333;"">Tu contraseña ha sido cambiada con éxito</h1>
                        </td>
                        </tr>
                        <tr>
                        <td style=""color: #555555; font-size: 16px; line-height: 1.5;"">
                            <p style=""margin-bottom: 20px;"">
                                Queremos informarte que la contraseña de tu cuenta fue cambiada recientemente. Si fuiste tú quien realizó este cambio, no necesitas hacer nada más. Si no reconoces esta actividad, por favor restablece tu contraseña de inmediato.
                            </p>
                            <p style=""text-align: center; margin: 30px 0;"">
                            <a href=""https://localhost:7276/Cuenta/Recuperar"" style=""display: inline-block; background-color: #FF0000; color: #ffffff; padding: 14px 28px; text-decoration: none; border-radius: 6px; font-weight: bold;"">
                                Recuperar contraseña
                            </a>
                            </p>
                            <p style=""margin-top: 30px; color: #888888; font-size: 14px;"">
                            Por seguridad, nunca compartas tu contraseña con nadie.
                            </p>
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

        public static string RestringirAccesoHtml(string rol)
        {
            return $@"
            <body style=""margin: 0; padding: 0; font-family: 'Segoe UI', sans-serif; background-color: #f6f9fc;"">
                <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                    <tr>
                    <td align=""center"" style=""padding: 40px 0;"">
                        <table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 5px rgba(0,0,0,0.05); padding: 40px;"">
                        <tr>
                            <td align=""center"" style=""padding-bottom: 20px;"">
                            <h1 style=""margin: 0; font-size: 24px; color: #d32f2f;"">Restringimos su acceso como <strong>{rol}</strong></h1>
                            </td>
                        </tr>
                        <tr>
                            <td style=""color: #555555; font-size: 16px; line-height: 1.5;"">
                            <p style=""margin-bottom: 20px;"">
                                Hemos detectado actividad inusual en su cuenta y por medidas de seguridad hemos aplicado restricciones temporales.
                            </p>
                    
                            <p style=""margin-top: 30px; color: #888888; font-size: 14px;"">
                                Si considera que esto es un error, por favor responda a este correo inmediatamente.
                            </p>
                            </td>
                        </tr>
                        <tr>
                            <td style=""text-align: center; font-size: 12px; color: #aaaaaa;"">
                            © {DateTime.Now.Year} - Portal COSIE.
                            </td>
                        </tr>
                        </table>
                    </td>
                    </tr>
                </table>
            </body>";
        }

        public static string ActivarAccesoHtml(string rol)
        {
            return $@"
            <body style=""margin: 0; padding: 0; font-family: 'Segoe UI', sans-serif; background-color: #f6f9fc;"">
                <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                    <tr>
                    <td align=""center"" style=""padding: 40px 0;"">
                        <table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 5px rgba(0,0,0,0.05); padding: 40px;"">
                        <tr>
                            <td align=""center"" style=""padding-bottom: 20px;"">
                            <h1 style=""margin: 0; font-size: 24px; color: #2e7d32;"">¡Bienvenido!</h1>
                            </td>
                        </tr>
                        <tr>
                            <td style=""color: #555555; font-size: 16px; line-height: 1.5;"">
                            <p style=""margin-bottom: 20px; text-align: center;"">
                                Su cuenta ha sido activada exitosamente como <strong>{rol}</strong>, ya puedes hacer uso de la plataforma.
                            </p>
                            <p style=""text-align: center; margin: 30px 0;"">
                                <a href=""https://localhost:7276/Cuenta/Ingresar"" style=""display: inline-block; background-color: #2e7d32; color: #ffffff; padding: 14px 28px; text-decoration: none; border-radius: 6px; font-weight: bold;"">
                                Iniciar sesión
                                </a>
                            </p>
                            </td>
                        </tr>
                        <tr>
                            <td style=""text-align: center; font-size: 12px; color: #aaaaaa;"">
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
