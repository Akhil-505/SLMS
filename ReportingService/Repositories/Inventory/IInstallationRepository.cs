using ReportingService.Models;

namespace ReportingService.Repositories.Inventory
{
    public interface IInstallationRepository
    {
        Task<List<InstalledSoftwareDto>> GetAllAsync();
    }
}
