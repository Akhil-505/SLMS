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

        public async Task<InstalledSoftware> InstallAsync(InstalledSoftware install, string performedBy)
        {
            await _repo.AddAsync(install);

            var history = new InstallationHistory
            {
                InstalledSoftwareId = install.Id,
                Action = "Installed",
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
                InstalledSoftwareId = install.Id,
                Action = "Uninstalled",
                PerformedBy = performedBy,
                Timestamp = DateTime.UtcNow
            };

            await _historyRepo.AddAsync(history);
            await _repo.DeleteAsync(install);

            return true;
        }
    }
}
