using ComplianceService.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace ComplianceService.Repositories.Database
{
    public class RenewalRepository : IRenewalRepository
    {
        private readonly ComplianceDbContext _db;

        public RenewalRepository(ComplianceDbContext db)
        {
            _db = db;
        }

        public Task<List<RenewalEntity>> GetAllAsync() =>
            _db.Renewals.OrderBy(r => r.ExpiryDate).ToListAsync();

        public Task<RenewalEntity?> GetByIdAsync(int id) =>
            _db.Renewals.FirstOrDefaultAsync(r => r.Id == id);

        public async Task AddAsync(RenewalEntity entity)
        {
            await _db.Renewals.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(RenewalEntity entity)
        {
            _db.Renewals.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var renewal = await _db.Renewals.FindAsync(id);
            if (renewal != null)
            {
                _db.Renewals.Remove(renewal);
                await _db.SaveChangesAsync();
            }
        }
    }
}
