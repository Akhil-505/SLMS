using ComplianceService.Models;

namespace ComplianceService.Repositories.Inventory
{
    public interface ICatalogDataRepository
    {
        Task<List<SoftwareCatalogDto>> GetCatalogAsync();
    }
}
