using InventoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Repositories
{
    public class SoftwareCatalogRepository : ISoftwareCatalogRepository
    {
        private readonly AppDbContext _db;
        public SoftwareCatalogRepository(AppDbContext db) => _db = db;

        public Task<List<SoftwareCatalog>> GetAllAsync() => _db.SoftwareCatalog.ToListAsync();

        public Task<SoftwareCatalog?> GetByIdAsync(int id) =>
            _db.SoftwareCatalog.FirstOrDefaultAsync(s => s.Id == id);

        public async Task AddAsync(SoftwareCatalog item)
        {
            await _db.SoftwareCatalog.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(SoftwareCatalog item)
        {
            _db.SoftwareCatalog.Update(item);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(SoftwareCatalog item)
        {
            _db.SoftwareCatalog.Remove(item);
            await _db.SaveChangesAsync();
        }
    }
}
