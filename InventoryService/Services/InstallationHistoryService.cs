using InventoryService.Models;
using InventoryService.Repositories;

namespace InventoryService.Services
{
    public class InstallationHistoryService : IInstallationHistoryService
    {
        private readonly IInstallationHistoryRepository _repo;

        public InstallationHistoryService(IInstallationHistoryRepository repo)
        {
            _repo = repo;
        }

        public Task<List<InstallationHistory>> GetForInstallationAsync(int installedSoftwareId) =>
            _repo.GetByInstalledIdAsync(installedSoftwareId);
    }
}
