using Microsoft.Extensions.Configuration;
using PortalCOSIE.Application;
using PortalCOSIE.Application.Services;
using System.Net;
using System.Net.Mail;

namespace PortalCOSIE.Infrastructure.QueryService
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public SmtpEmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task<Result<string>> SendEmailAsync(string toEmail, string subject, string message)
        {
            var smtpHost = _config["Smtp:Host"];
            var smtpPort = _config["Smtp:Port"];
            var smtpUser = _config["Smtp:User"];
            var smtpPass = _config["Smtp:Pass"];
            var smtpFrom = _config["Smtp:From"];

            if (string.IsNullOrEmpty(smtpHost) || string.IsNullOrEmpty(smtpPort) ||
                string.IsNullOrEmpty(smtpUser) || string.IsNullOrEmpty(smtpPass) ||
                string.IsNullOrEmpty(smtpFrom))
            {
                throw new InvalidOperationException("SMTP configuration is missing or invalid.");
            }

            var smtpClient = new SmtpClient(smtpHost, int.Parse(smtpPort))
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true,
            };

            var fromAdress = new MailAddress(smtpFrom, "Gestión escolar");
            var toAdress = new MailAddress(toEmail);

            var mailMessage = new MailMessage(fromAdress, toAdress)
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            await smtpClient.SendMailAsync(mailMessage);
            return Result<string>.Success("Correo enviado correctamente");
        }
    }

}
