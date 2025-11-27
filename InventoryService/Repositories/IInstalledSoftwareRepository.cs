using InventoryService.Models;

namespace InventoryService.Repositories
{
    public interface IInstalledSoftwareRepository
    {
        Task<List<InstalledSoftware>> GetAllAsync();
        Task<InstalledSoftware?> GetByIdAsync(int id);
        Task AddAsync(InstalledSoftware install);
        Task UpdateAsync(InstalledSoftware install);
        Task DeleteAsync(InstalledSoftware install);

        Task<List<InstalledSoftware>> GetByDeviceIdAsync(int deviceId);
    }
}
