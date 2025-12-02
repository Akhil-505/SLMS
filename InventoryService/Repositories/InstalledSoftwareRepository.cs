using InventoryService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace InventoryService.Repositories
{
    public class InstalledSoftwareRepository : IInstalledSoftwareRepository
    {
        private readonly AppDbContext _db;
        public InstalledSoftwareRepository(AppDbContext db) => _db = db;

        public Task<List<InstalledSoftware>> GetAllAsync() =>
            _db.InstalledSoftware
               .Include(i => i.Device)
               .Include(i => i.License)
               .ToListAsync();

        public Task<InstalledSoftware?> GetByIdAsync(int id) =>
            _db.InstalledSoftware
               .Include(i => i.Device)
               .Include(i => i.License)
               .Include(i => i.History)
               .FirstOrDefaultAsync(i => i.Id == id);

        public Task<List<InstalledSoftware>> GetByDeviceIdAsync(int deviceId) =>
            _db.InstalledSoftware
               .Where(i => i.DeviceId == deviceId)
               .Include(i => i.License)
               .Include(i => i.History)
               .ToListAsync();

        public async Task AddAsync(InstalledSoftware install)
        {
            await _db.InstalledSoftware.AddAsync(install);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(InstalledSoftware install)
        {
            _db.InstalledSoftware.Update(install);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(InstalledSoftware install)
        {
            _db.InstalledSoftware.Remove(install);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<InstalledSoftware, bool>> predicate)
        {
            return await _db.InstalledSoftware.AnyAsync(predicate);
        }
    }
}
