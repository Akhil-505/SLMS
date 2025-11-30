using ComplianceService.Models;

namespace ComplianceService.Repositories
{
    public interface IComplianceEventRepository
    {
        Task AddAsync(ComplianceEvent evt);
        Task<IEnumerable<ComplianceEvent>> GetAllAsync();
        Task<ComplianceEvent?> GetExistingOpenEventAsync(int licenseId, string eventType);

    }
}
