using ComplianceService.Models;

namespace ComplianceService.Repositories.Inventory
{
    public interface IInstallationDataRepository
    {
        Task<List<InstalledSoftwareDto>> GetAllInstallationsAsync();
    }
}
