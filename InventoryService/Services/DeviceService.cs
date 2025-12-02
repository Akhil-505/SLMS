using InventoryService.Models;
using InventoryService.Repositories;

namespace InventoryService.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _repo;

        public DeviceService(IDeviceRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Device>> GetAllAsync() => _repo.GetAllAsync();

        public Task<Device?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public Task<Device?> GetByDeviceIdAsync(string deviceId) => _repo.GetByDeviceIdAsync(deviceId);

        public async Task<Device> CreateAsync(Device device)
        {
            await _repo.AddAsync(device);
            return device;
        }

        public async Task<Device> UpdateAsync(int id, Device update)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) throw new Exception("Device not found");

            existing.Hostname = update.Hostname;
            existing.DeviceId= update.DeviceId;
            existing.Department = update.Department;
            existing.Location = update.Location;
            existing.OwnerUserId = update.OwnerUserId;

            await _repo.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var device = await _repo.GetByIdAsync(id);
            if (device == null) return false;

            await _repo.DeleteAsync(device);
            return true;
        }

        public Task<List<InstalledSoftware>> GetInstalledSoftwareAsync(int deviceId) =>
            _repo.GetInstalledSoftwareAsync(deviceId);
    }
}
