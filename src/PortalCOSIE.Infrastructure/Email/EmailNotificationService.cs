using PortalCOSIE.Application;
using PortalCOSIE.Application.Services.Notificacion;

namespace PortalCOSIE.Infrastructure.Email
{
    public class EmailNotificationService : INotificationService
    {
        private readonly IEmailSender _emailSender;
        private readonly IMessageTemplateService _plantillas;

        public EmailNotificationService(IEmailSender emailSender, IMessageTemplateService plantillas)
        {
            _emailSender = emailSender;
            _plantillas = plantillas;
        }

        public Task<Result<string>> EnviarAsync(
            string destinatario,
            string asunto,
            Func<IMessageTemplateService, string> construirPlantilla)
            => _emailSender.SendEmailAsync(destinatario, asunto, construirPlantilla(_plantillas));
    }
}