using Microsoft.EntityFrameworkCore;

namespace ComplianceService.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        public DbSet<ComplianceEvent> ComplianceEvents { get; set; }
    }
}
