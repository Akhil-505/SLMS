using ComplianceService.Models.Database;

namespace ComplianceService.Repositories.Database
{
    public interface IComplianceEventRepository
    {
        Task<List<ComplianceEventEntity>> GetAllAsync();
        Task<List<ComplianceEventEntity>> GetUnresolvedAsync();
        Task<ComplianceEventEntity?> GetByIdAsync(int id);
        Task AddAsync(ComplianceEventEntity entity);
        Task UpdateAsync(ComplianceEventEntity entity);
    }
}
