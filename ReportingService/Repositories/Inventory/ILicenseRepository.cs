using ReportingService.Models;

namespace ReportingService.Repositories.Inventory
{
    public interface ILicenseRepository
    {
        Task<List<LicenseDto>> GetAllAsync();
    }
}
