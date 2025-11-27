using InventoryService.Models;
using InventoryService.Repositories;

namespace InventoryService.Services
{
    public class SoftwareCatalogService : ISoftwareCatalogService
    {
        private readonly ISoftwareCatalogRepository _repo;

        public SoftwareCatalogService(ISoftwareCatalogRepository repo)
        {
            _repo = repo;
        }

        public Task<List<SoftwareCatalog>> GetAllAsync() => _repo.GetAllAsync();
        public Task<SoftwareCatalog?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task<SoftwareCatalog> CreateAsync(SoftwareCatalog item)
        {
            await _repo.AddAsync(item);
            return item;
        }

        public async Task<SoftwareCatalog> UpdateAsync(int id, SoftwareCatalog update)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) throw new Exception("Catalog item not found");

            existing.ProductName = update.ProductName;
            existing.Vendor = update.Vendor;
            existing.Description = update.Description;
            existing.Category = update.Category;
            existing.Status = update.Status;
            existing.SKU = update.SKU;

            await _repo.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return false;

            await _repo.DeleteAsync(item);
            return true;
        }
    }
}
