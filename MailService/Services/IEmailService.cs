using MailService.Models;

namespace MailService.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailRequest request);
    }
}
