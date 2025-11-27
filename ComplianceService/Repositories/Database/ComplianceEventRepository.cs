using ComplianceService.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace ComplianceService.Repositories.Database
{
    public class ComplianceEventRepository : IComplianceEventRepository
    {
        private readonly ComplianceDbContext _db;

        public ComplianceEventRepository(ComplianceDbContext db)
        {
            _db = db;
        }

        public Task<List<ComplianceEventEntity>> GetAllAsync() =>
            _db.ComplianceEvents.OrderByDescending(e => e.CreatedAt).ToListAsync();

        public Task<List<ComplianceEventEntity>> GetUnresolvedAsync() =>
            _db.ComplianceEvents.Where(e => !e.Resolved).OrderByDescending(e => e.CreatedAt).ToListAsync();

        public Task<ComplianceEventEntity?> GetByIdAsync(int id) =>
            _db.ComplianceEvents.FirstOrDefaultAsync(e => e.Id == id);

        public async Task AddAsync(ComplianceEventEntity entity)
        {
            await _db.ComplianceEvents.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(ComplianceEventEntity entity)
        {
            _db.ComplianceEvents.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}
