using InventoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly AppDbContext _db;
        public DeviceRepository(AppDbContext db) => _db = db;

        public Task<List<Device>> GetAllAsync() =>
            _db.Devices.Include(d => d.OwnerUser).ToListAsync();

        public Task<Device?> GetByIdAsync(int id) =>
            _db.Devices
               .Include(d => d.OwnerUser)
               .FirstOrDefaultAsync(d => d.Id == id);

        public Task<Device?> GetByDeviceIdAsync(string deviceId) =>
            _db.Devices
               .Include(d => d.OwnerUser)
               .Include(d => d.InstalledSoftware)
               .ThenInclude(i => i.License)
               .FirstOrDefaultAsync(d => d.DeviceId == deviceId);

        public async Task AddAsync(Device device)
        {
            await _db.Devices.AddAsync(device);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Device device)
        {
            _db.Devices.Update(device);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Device device)
        {
            _db.Devices.Remove(device);
            await _db.SaveChangesAsync();
        }

        public Task<List<InstalledSoftware>> GetInstalledSoftwareAsync(int deviceId) =>
            _db.InstalledSoftware
               .Include(i => i.License)
               .Where(i => i.DeviceId == deviceId)
               .ToListAsync();

        public async Task<IEnumerable<Device>> GetDevicesLastSeenBeforeAsync(DateTime cutoffDate)
        {
            // SQL Translation: SELECT * FROM Devices WHERE LastSeen < @cutoffDate
            return await _db.Devices
                                 .Where(d => d.LastSeen < cutoffDate)
                                 .ToListAsync();
        }
    }
}
