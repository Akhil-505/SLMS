using Microsoft.EntityFrameworkCore;

namespace InventoryService.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<Device> Devices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Entitlement> Entitlements { get; set; }
        public DbSet<SoftwareCatalog> SoftwareCatalog { get; set; }
        public DbSet<InstalledSoftware> InstalledSoftware { get; set; }
        public DbSet<InstallationHistory> InstallationHistories { get; set; }
        public DbSet<VendorContract> VendorContracts { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<User>()
                .HasMany(u => u.Devices)
                .WithOne(d => d.OwnerUser)
                .HasForeignKey(d => d.OwnerUserId)
                .OnDelete(DeleteBehavior.SetNull);

            mb.Entity<License>()
                .HasOne(l => l.VendorContract)
                .WithOne(vc => vc.License)
                .HasForeignKey<VendorContract>(vc => vc.LicenseId);

            mb.Entity<Device>()
                .HasMany(d => d.InstalledSoftware)
                .WithOne(i => i.Device);

            mb.Entity<InstalledSoftware>()
                .HasMany(i => i.History)
                .WithOne(h => h.InstalledSoftware);

            base.OnModelCreating(mb);
        }
    }
}
