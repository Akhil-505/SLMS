using InventoryService.Models;
using InventoryService.Repositories;

namespace InventoryService.Services
{
    public class InstallationService : IInstallationService
    {
        private readonly IInstalledSoftwareRepository _repo;
        private readonly IInstallationHistoryRepository _historyRepo;

        public InstallationService(
            IInstalledSoftwareRepository repo,
            IInstallationHistoryRepository historyRepo)
        {
            _repo = repo;
            _historyRepo = historyRepo;
        }

        public Task<List<InstalledSoftware>> GetAllAsync() => _repo.GetAllAsync();

        public Task<InstalledSoftware?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task<InstalledSoftware> InstallAsync(
     InstalledSoftware install,
     string performedBy,
     string action)
        {
            // 🔍 1. Check if installation already exists for same Device + License + Product
            //var exists = await _repo.ExistsAsync(x =>
            //    x.DeviceId == install.DeviceId &&
                
            //    x.LicenseId == install.LicenseId &&
            //    x.ProductName.ToLower() == install.ProductName.ToLower()
            //);

            //if (exists)
            //    throw new InvalidOperationException("Installation already exists for this device.");

            // ✅ 2. Create installation now (safe)
            await _repo.AddAsync(install);

            // 📌 3. Create installation history entry
            var history = new InstallationHistory
            {
                InstalledSoftwareId = install.InstalledSoftwareId,  // <-- Important: use install.Id
                Action = action,
                PerformedBy = performedBy,
                Timestamp = DateTime.UtcNow
            };

            await _historyRepo.AddAsync(history);

            return install;
        }

        public async Task<bool> UninstallAsync(int id, string performedBy)
        {
            var install = await _repo.GetByIdAsync(id);
            if (install == null) return false;

            var history = new InstallationHistory
            {
                InstalledSoftwareId = install.InstalledSoftwareId,
                Action = "Uninstalled",
                PerformedBy = performedBy,
                Timestamp = DateTime.UtcNow
            };

            //await _historyRepo.AddAsync(history);
            await _repo.DeleteAsync(install);

            return true;
        }
    }
}
