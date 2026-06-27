namespace PortalCOSIE.Application.Services.Notificacion
{
    public interface INotificationService
    {
        Task<Result<string>> EnviarAsync(
            string destinatario,
            string asunto,
            Func<IMessageTemplateService, string> construirPlantilla);
    }
}