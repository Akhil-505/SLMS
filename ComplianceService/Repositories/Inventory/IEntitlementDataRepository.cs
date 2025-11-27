using ComplianceService.Models;

namespace ComplianceService.Repositories.Inventory
{
    public interface IEntitlementDataRepository
    {
        Task<List<EntitlementDto>> GetAllEntitlementsAsync();
    }
}
