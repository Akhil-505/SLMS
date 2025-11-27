using InventoryService.Models;

namespace InventoryService.Services
{
    public interface IDeviceService
    {
        Task<List<Device>> GetAllAsync();
        Task<Device?> GetByIdAsync(int id);
        Task<Device?> GetByDeviceIdAsync(string deviceId);
        Task<Device> CreateAsync(Device device);
        Task<Device> UpdateAsync(int id, Device update);
        Task<bool> DeleteAsync(int id);
        Task<List<InstalledSoftware>> GetInstalledSoftwareAsync(int deviceId);
    }
}
