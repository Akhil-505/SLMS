using MailService.Models;

namespace MailService.Repositories
{
    public interface IEmailRepository
    {
        Task AddLogAsync(EmailLog log);
        Task<IEnumerable<EmailLog>> GetAllAsync();
    }
}