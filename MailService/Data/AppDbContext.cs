using MailService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MailService.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<EmailLog> EmailLogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
