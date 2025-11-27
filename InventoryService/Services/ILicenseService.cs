using InventoryService.Models;

namespace InventoryService.Services
{
    public interface ILicenseService
    {
        Task<List<License>> GetAllAsync();
        Task<License?> GetByIdAsync(int id);
        Task<License> CreateAsync(License license);
        Task<License> UpdateAsync(int id, License update);
        Task<bool> DeleteAsync(int id);
        Task<List<License>> SearchAsync(string? product, string? vendor);
    }
}
