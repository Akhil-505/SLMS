using ReportingService.Models;

namespace ReportingService.Repositories.Inventory
{
    public interface IEntitlementRepository
    {
        Task<List<EntitlementDto>> GetAllAsync();
    }
}
