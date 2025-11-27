using InventoryService.Models;

namespace InventoryService.Repositories
{
    public interface IInstallationHistoryRepository
    {
        Task<List<InstallationHistory>> GetByInstalledIdAsync(int installedId);
        Task AddAsync(InstallationHistory history);
    }
}
