using InventoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Repositories
{
    public class InstallationHistoryRepository : IInstallationHistoryRepository
    {
        private readonly AppDbContext _db;
        public InstallationHistoryRepository(AppDbContext db) => _db = db;

        public Task<List<InstallationHistory>> GetByInstalledIdAsync(int installedId) =>
            _db.InstallationHistories
               .Where(h => h.InstalledSoftwareId == installedId)
               .ToListAsync();

        public async Task AddAsync(InstallationHistory history)
        {
            await _db.InstallationHistories.AddAsync(history);
            await _db.SaveChangesAsync();
        }
    }
}
