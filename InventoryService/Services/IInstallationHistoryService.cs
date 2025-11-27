using InventoryService.Models;

namespace InventoryService.Services
{
    public interface IInstallationHistoryService
    {
        Task<List<InstallationHistory>> GetForInstallationAsync(int installedSoftwareId);
    }
}
