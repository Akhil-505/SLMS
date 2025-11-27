using InventoryService.Models;
using InventoryService.Repositories;

namespace InventoryService.Services
{
    public class LicenseService : ILicenseService
    {
        private readonly ILicenseRepository _repo;

        public LicenseService(ILicenseRepository repo)
        {
            _repo = repo;
        }

        public Task<List<License>> GetAllAsync() => _repo.GetAllAsync();

        public Task<License?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task<License> CreateAsync(License license)
        {
            await _repo.AddAsync(license);
            return license;
        }

        public async Task<License> UpdateAsync(int id, License update)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) throw new Exception("License not found");

            existing.ProductName = update.ProductName;
            existing.Vendor = update.Vendor;
            existing.SKU = update.SKU;
            existing.LicenseType = update.LicenseType;
            existing.TotalEntitlements = update.TotalEntitlements;
            existing.ExpiryDate = update.ExpiryDate;
            existing.Cost = update.Cost;

            await _repo.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var license = await _repo.GetByIdAsync(id);
            if (license == null) return false;

            await _repo.DeleteAsync(license);
            return true;
        }

        public Task<List<License>> SearchAsync(string? product, string? vendor) =>
            _repo.SearchAsync(product, vendor);
    }
}
