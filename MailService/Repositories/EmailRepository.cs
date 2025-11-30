using MailService.Data;
using MailService.Models;
using Microsoft.EntityFrameworkCore;

namespace MailService.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly AppDbContext _context;

        public EmailRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddLogAsync(EmailLog log)
        {
            await _context.EmailLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmailLog>> GetAllAsync()
        {
            return await _context.EmailLogs.ToListAsync();
        }
    }
}
