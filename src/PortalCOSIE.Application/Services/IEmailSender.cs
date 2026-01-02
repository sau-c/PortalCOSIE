namespace PortalCOSIE.Application.Services
{
    public interface IEmailSender
    {
        Task<Result<string>> SendEmailAsync(string toEmail, string subject, string htmlMessage);
    }
}
