namespace PortalCOSIE.Application.Interfaces
{
    public interface IEmailSender
    {
        Task<Result<string>> SendEmailAsync(string toEmail, string subject, string htmlMessage);
    }
}
