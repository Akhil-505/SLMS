using InventoryService.Models;

namespace InventoryService.Repositories
{
    public interface IEntitlementRepository
    {
        Task<List<Entitlement>> GetAllAsync();
        Task<Entitlement?> GetByIdAsync(int id);

        Task AddAsync(Entitlement entitlement);
        Task UpdateAsync(Entitlement entitlement);
        Task DeleteAsync(Entitlement entitlement);

        Task<int> CountAssignmentsForLicense(int licenseId);
        Task<List<Entitlement>> GetByLicenseIdAsync(int licenseId);
    }
}
