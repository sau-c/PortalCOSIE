namespace PortalCOSIE.Application.Services.Notificacion
{
    public interface IEmailSender
    {
        Task<Result<string>> SendEmailAsync(string toEmail, string asunto, string mensaje);
    }
}
