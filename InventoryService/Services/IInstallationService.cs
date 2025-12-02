using InventoryService.Models;

namespace InventoryService.Services
{
    public interface IInstallationService
    {
        Task<List<InstalledSoftware>> GetAllAsync();
        Task<InstalledSoftware?> GetByIdAsync(int id);

        Task<InstalledSoftware> InstallAsync(InstalledSoftware install, string performedBy,string Action);
        Task<bool> UninstallAsync(int id, string performedBy);
    }
}
