using InventoryService.Models;

namespace InventoryService.Services
{
    public interface IEntitlementService
    {
        Task<List<Entitlement>> GetAllAsync();
        Task<Entitlement?> GetByIdAsync(int id);
        Task<Entitlement> AssignAsync(Entitlement entitlement);
        Task<bool> DeleteAsync(int id);
    }
}
