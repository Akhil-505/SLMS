using ComplianceService.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplianceService.Repositories
{
    public class ComplianceEventRepository : IComplianceEventRepository
    {
        private readonly AppDbContext _db;

        public ComplianceEventRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(ComplianceEvent evt)
        {
            await _db.ComplianceEvents.AddAsync(evt);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ComplianceEvent>> GetAllAsync()
        {
            return await _db.ComplianceEvents
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }
        public async Task<ComplianceEvent?> GetExistingOpenEventAsync(int licenseId, string eventType)
        {
            return await _db.ComplianceEvents
                .Where(e => e.LicenseId == licenseId &&
                            e.EventType == eventType &&
                            e.Resolved == false)
                .FirstOrDefaultAsync();
        }

    }
}
