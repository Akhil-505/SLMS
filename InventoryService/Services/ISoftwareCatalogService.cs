using InventoryService.Models;

namespace InventoryService.Services
{
    public interface ISoftwareCatalogService
    {
        Task<List<SoftwareCatalog>> GetAllAsync();
        Task<SoftwareCatalog?> GetByIdAsync(int id);
        Task<SoftwareCatalog> CreateAsync(SoftwareCatalog item);
        Task<SoftwareCatalog> UpdateAsync(int id, SoftwareCatalog update);
        Task<bool> DeleteAsync(int id);
    }
}
