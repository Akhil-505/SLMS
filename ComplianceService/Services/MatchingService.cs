using ComplianceService.Models;

namespace ComplianceService.Services
{
    public class MatchingService
    {
        public int GetUsageCount(
            LicenseModel lic,
            IEnumerable<InstalledSoftwareModel> installs)
        {
            return installs.Count(i =>
                i.LicenseId == lic.LicenseId &&
                i.ProductName.Equals(lic.ProductName, StringComparison.OrdinalIgnoreCase)
            );
        }
    }
}
