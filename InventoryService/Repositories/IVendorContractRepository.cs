using InventoryService.Models;

namespace InventoryService.Repositories
{
    public interface IVendorContractRepository
    {
        Task<VendorContract?> GetByLicenseIdAsync(int licenseId);
        Task AddAsync(VendorContract contract);
        Task UpdateAsync(VendorContract contract);
        Task DeleteAsync(VendorContract contract);
    }
}
