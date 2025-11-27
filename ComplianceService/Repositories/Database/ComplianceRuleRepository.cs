using ComplianceService.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace ComplianceService.Repositories.Database
{
    public class ComplianceRuleRepository : IComplianceRuleRepository
    {
        private readonly ComplianceDbContext _db;

        public ComplianceRuleRepository(ComplianceDbContext db)
        {
            _db = db;
        }

        public Task<List<ComplianceRuleEntity>> GetAllAsync() =>
            _db.ComplianceRules.OrderBy(r => r.Name).ToListAsync();

        public Task<ComplianceRuleEntity?> GetByIdAsync(int id) =>
            _db.ComplianceRules.FirstOrDefaultAsync(r => r.Id == id);

        public async Task AddAsync(ComplianceRuleEntity entity)
        {
            await _db.ComplianceRules.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(ComplianceRuleEntity entity)
        {
            _db.ComplianceRules.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var rule = await _db.ComplianceRules.FindAsync(id);
            if (rule != null)
            {
                _db.ComplianceRules.Remove(rule);
                await _db.SaveChangesAsync();
            }
        }
    }
}
