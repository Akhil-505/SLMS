using InventoryService.Models;
using InventoryService.Repositories;

namespace InventoryService.Repositories
{
    public interface IDeviceRepository
    {
        Task<List<Device>> GetAllAsync();
        Task<Device?> GetByIdAsync(int id);
        Task<Device?> GetByDeviceIdAsync(string deviceId);
        Task AddAsync(Device device);
        Task UpdateAsync(Device device);
        Task DeleteAsync(Device device);
        Task<List<InstalledSoftware>> GetInstalledSoftwareAsync(int deviceId);
    }
}
