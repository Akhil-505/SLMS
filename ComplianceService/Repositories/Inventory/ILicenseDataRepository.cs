using ComplianceService.Models;

namespace ComplianceService.Repositories.Inventory
{
    public interface ILicenseDataRepository
    {
        Task<List<LicenseDto>> GetAllLicensesAsync();
    }
}
