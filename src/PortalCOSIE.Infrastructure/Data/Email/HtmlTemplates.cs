namespace PortalCOSIE.Infrastructure
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
                            <h1 style=""margin: 0; font-size: 24px; color: #333333;"">Confirma tu correo</h1>
                            </td>
                        </tr>
                        <tr>
                            <td style=""color: #555555; font-size: 16px; line-height: 1.5;"">
                            <p style=""margin-bottom: 20px;"">
                                Para completar tu petición, por favor confirma tu correo electrónico haciendo clic en el siguiente botón:
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
        public static string VerificarCorreoHtml(string userId, string correo, string encodedToken)
        {
            return $@"
            <body style=""margin:0; padding:0; font-family:'Segoe UI', sans-serif; background-color:#f4f6f8;"">
                <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""background-color:#f4f6f8; padding:40px 0;"">
                    <tr>
                        <td align=""center"">
                            <table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""background:#ffffff; border-radius:10px; padding:40px; box-shadow:0 3px 8px rgba(0,0,0,0.08);"">
                        
                                <tr>
                                    <td align=""center"" style=""padding-bottom:25px;"">
                                        <h1 style=""margin:0; font-size:26px; color:#2d2d2d;"">
                                            Solicitud de actualización de correo
                                        </h1>
                                    </td>
                                </tr>

                                <tr>
                                    <td style=""font-size:16px; color:#4a4a4a; line-height:1.6;"">
                                        <p>
                                            Hemos recibido una solicitud para cambiar el correo asociado a tu cuenta del 
                                            <strong>Portal COSIE</strong> a este correo (<strong>{correo}</strong>).
                                        </p>

                                        <p>
                                            Si tú realizaste esta solicitud, por favor confirma el cambio dando clic en el siguiente botón:
                                        </p>

                                        <div style=""text-align:center; margin:35px 0;"">
                                            <a href=""https://localhost:7276/Cuenta/ActualizarCorreo?id={userId}&correo={correo}&token={encodedToken}""
                                               style=""background-color:#1d72b8; color:#ffffff; padding:14px 30px; text-decoration:none; 
                                               border-radius:8px; font-size:16px; font-weight:bold; display:inline-block;"">
                                                Confirmar cambio
                                            </a>
                                        </div>

                                        <p style=""color:#777; font-size:14px;"">
                                            Si no solicitaste este cambio, puedes ignorar este mensaje.
                                            Tu cuenta seguirá vinculada a tu correo actual.
                                        </p>
                                    </td>
                                </tr>

                                <tr>
                                    <td style=""text-align:center; padding-top:35px; font-size:12px; color:#999;"">
                                        © {DateTime.Now.Year} - Portal COSIE.
                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>
                </table>
            </body>";
        }
        public static string CorreoActualizadoHtml(string nuevoCorreo)
        {
            return $@"
            <body style=""margin:0; padding:0; font-family: 'Segoe UI', sans-serif; background-color:#f6f9fc;"">
                <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                    <tr>
                        <td align=""center"" style=""padding:40px 0;"">
                            <table width=""600"" cellpadding=""0"" cellspacing=""0"" 
                                   style=""background-color:#ffffff; border-radius:8px; 
                                          box-shadow:0 2px 5px rgba(0,0,0,0.05); padding:40px;"">
                        
                                <!-- Título -->
                                <tr>
                                    <td align=""center"" style=""padding-bottom:20px;"">
                                        <h1 style=""margin:0; font-size:24px; color:#333333;"">
                                            Tu correo ha sido actualizado
                                        </h1>
                                    </td>
                                </tr>

                                <!-- Contenido -->
                                <tr>
                                    <td style=""color:#555555; font-size:16px; line-height:1.6;"">
                                
                                        <p>
                                            Queremos informarte que el correo asociado a tu cuenta 
                                            ha sido actualizado correctamente.
                                        </p>

                                        <p style=""margin:20px 0; font-weight:bold; color:#1d72b8;"">
                                            Nuevo correo registrado: <span style=""color:#333333;"">{nuevoCorreo}</span>
                                        </p>

                                        <p>
                                            Si tú realizaste esta acción, no necesitas hacer nada más.
                                        </p>

                                        <p style=""margin-top:25px;"">
                                            <strong>¿No reconoces este cambio?</strong><br>
                                            Por favor contacta inmediatamente a getión escolar.
                                        </p>

                                    </td>
                                </tr>

                                <!-- Footer -->
                                <tr>
                                    <td style=""padding-top:30px; text-align:center; font-size:12px; color:#aaaaaa;"">
                                        © {DateTime.Now.Year} - Portal COSIE.
                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>
                </table>
            </body>";
        }
        public static string CelularActualizadoHtml(string nuevoCelular)
        {
            return $@"
            <body style=""margin: 0; padding: 0; font-family: 'Segoe UI', sans-serif; background-color: #f6f9fc;"">
                <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                    <tr>
                        <td align=""center"" style=""padding: 40px 0;"">
                            <table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 5px rgba(0,0,0,0.05); padding: 40px;"">
                    
                                <tr>
                                    <td align=""center"" style=""padding-bottom: 20px;"">
                                        <h1 style=""margin: 0; font-size: 24px; color: #333333;"">
                                            Tu número de celular ha sido actualizado
                                        </h1>
                                    </td>
                                </tr>

                                <tr>
                                    <td style=""color: #555555; font-size: 16px; line-height: 1.5; text-align: justify;"">

                                        <p style=""margin-bottom: 20px;"">
                                            Te informamos que el número de celular asociado a tu cuenta ha sido actualizado correctamente. 
                                            El nuevo número registrado es el siguiente:
                                        </p>

                                        <p style=""text-align: center; font-size: 20px; font-weight: bold; margin: 30px 0;"">
                                            {nuevoCelular}
                                        </p>

                                        <p style=""margin-top: 20px;"">
                                            Si realizaste este cambio, no necesitas hacer nada más.  
                                            Si no reconoces esta actividad, te recomendamos contactar a gestión escolar.
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
    }
}
