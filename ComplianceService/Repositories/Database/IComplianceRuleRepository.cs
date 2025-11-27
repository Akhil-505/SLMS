using ComplianceService.Models.Database;

namespace ComplianceService.Repositories.Database
{
    public interface IComplianceRuleRepository
    {
        Task<List<ComplianceRuleEntity>> GetAllAsync();
        Task<ComplianceRuleEntity?> GetByIdAsync(int id);
        Task AddAsync(ComplianceRuleEntity entity);
        Task UpdateAsync(ComplianceRuleEntity entity);
        Task DeleteAsync(int id);
    }
}
