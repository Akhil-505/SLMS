using InventoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Repositories
{
    public class VendorContractRepository : IVendorContractRepository
    {
        private readonly AppDbContext _db;
        public VendorContractRepository(AppDbContext db) => _db = db;

        public Task<VendorContract?> GetByLicenseIdAsync(int licenseId) =>
            _db.VendorContracts
               .Include(vc => vc.License)
               .FirstOrDefaultAsync(vc => vc.LicenseId == licenseId);

        public async Task AddAsync(VendorContract contract)
        {
            await _db.VendorContracts.AddAsync(contract);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(VendorContract contract)
        {
            _db.VendorContracts.Update(contract);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(VendorContract contract)
        {
            _db.VendorContracts.Remove(contract);
            await _db.SaveChangesAsync();
        }
    }
}
