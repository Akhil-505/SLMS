using InventoryService.Models;
using InventoryService.Repositories;

namespace InventoryService.Services
{
    public class VendorContractService : IVendorContractService
    {
        private readonly IVendorContractRepository _repo;
        private readonly ILicenseRepository _licenseRepo;

        public VendorContractService(
            IVendorContractRepository repo,
            ILicenseRepository licenseRepo)
        {
            _repo = repo;
            _licenseRepo = licenseRepo;
        }

        public Task<VendorContract?> GetByLicenseIdAsync(int licenseId) =>
            _repo.GetByLicenseIdAsync(licenseId);

        public async Task<VendorContract> CreateAsync(VendorContract contract)
        {
            var license = await _licenseRepo.GetByIdAsync(contract.LicenseId);
            if (license == null) throw new Exception("License not found");

            await _repo.AddAsync(contract);
            return contract;
        }

        public async Task<VendorContract> UpdateAsync(int licenseId, VendorContract updated)
        {
            var existing = await _repo.GetByLicenseIdAsync(licenseId);
            if (existing == null) throw new Exception("Vendor Contract not found");

            existing.ContractNumber = updated.ContractNumber;
            existing.PurchaseDate = updated.PurchaseDate;
            existing.ExpiryDate = updated.ExpiryDate;
            existing.Terms = updated.Terms;
            existing.Price = updated.Price;

            await _repo.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(int licenseId)
        {
            var existing = await _repo.GetByLicenseIdAsync(licenseId);
            if (existing == null) return false;

            await _repo.DeleteAsync(existing);
            return true;
        }
    }
}
    