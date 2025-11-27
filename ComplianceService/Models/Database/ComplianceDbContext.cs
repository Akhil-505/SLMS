using Microsoft.EntityFrameworkCore;

namespace ComplianceService.Models.Database
{
    public class ComplianceDbContext : DbContext
    {
        public ComplianceDbContext(DbContextOptions<ComplianceDbContext> options)
            : base(options) { }

        public DbSet<ComplianceEventEntity> ComplianceEvents { get; set; }
        public DbSet<ComplianceRuleEntity> ComplianceRules { get; set; }
        public DbSet<RenewalEntity> Renewals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComplianceEventEntity>()
                .HasIndex(e => e.LicenseId);

            modelBuilder.Entity<RenewalEntity>()
                .HasIndex(r => r.ExpiryDate);

            base.OnModelCreating(modelBuilder);
        }
    }
}
