using ReportingService.Models;

namespace ReportingService.Services
{
    public interface IReportingService
    {
        Task<List<LicenseUsageSummary>> GetLicenseUsageAsync();
        Task<ComplianceSummary> GetComplianceSummaryAsync();
        Task<DashboardReport> GetDashboardReportAsync();
    }
}
