using InventoryService.Models;

namespace InventoryService.Services
{
    public interface IVendorContractService
    {
        Task<VendorContract?> GetByLicenseIdAsync(int licenseId);
        Task<VendorContract> CreateAsync(VendorContract contract);
        Task<VendorContract> UpdateAsync(int licenseId, VendorContract update);
        Task<bool> DeleteAsync(int licenseId);
    }
}
