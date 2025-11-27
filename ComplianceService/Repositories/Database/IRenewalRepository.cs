using ComplianceService.Models.Database;

namespace ComplianceService.Repositories.Database
{
    public interface IRenewalRepository
    {
        Task<List<RenewalEntity>> GetAllAsync();
        Task<RenewalEntity?> GetByIdAsync(int id);
        Task AddAsync(RenewalEntity entity);
        Task UpdateAsync(RenewalEntity entity);
        Task DeleteAsync(int id);
    }
}
