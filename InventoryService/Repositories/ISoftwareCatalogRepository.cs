using InventoryService.Models;

namespace InventoryService.Repositories
{
    public interface ISoftwareCatalogRepository
    {
        Task<List<SoftwareCatalog>> GetAllAsync();
        Task<SoftwareCatalog?> GetByIdAsync(int id);
        Task AddAsync(SoftwareCatalog item);
        Task UpdateAsync(SoftwareCatalog item);
        Task DeleteAsync(SoftwareCatalog item);
    }
}
