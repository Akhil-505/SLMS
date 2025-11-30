using ReportingService.Models;
using ReportingService.Repositories.Inventory;
using ReportingService.Repositories.Compliance;

namespace ReportingService.Services
{
    public class ReportingService : IReportingService
    {
        private readonly ILicenseRepository _licenseRepo;
        private readonly IEntitlementRepository _entRepo;
        private readonly IInstallationRepository _installRepo;
        private readonly IComplianceReportRepository _complianceRepo;

        public ReportingService(
            ILicenseRepository licenseRepo,
            IEntitlementRepository entRepo,
            IInstallationRepository installRepo,
            IComplianceReportRepository complianceRepo)
        {
            _licenseRepo = licenseRepo;
            _entRepo = entRepo;
            _installRepo = installRepo;
            _complianceRepo = complianceRepo;
        }

        // --------------------------------------------------------
        // 1. LICENSE UTILIZATION SUMMARY
        // --------------------------------------------------------
        public async Task<List<LicenseUsageSummary>> GetLicenseUsageAsync()
        {
            var licenses = await _licenseRepo.GetAllAsync();
            var entitlements = await _entRepo.GetAllAsync();
            var installs = await _installRepo.GetAllAsync();

            var list = new List<LicenseUsageSummary>();

            foreach (var lic in licenses)
            {
                int assigned = entitlements.Count(e => e.LicenseId == lic.Id);
                int installed = installs.Count(i => i.LicenseId == lic.Id);

                int usage = Math.Max(assigned, installed);

                double utilization = lic.TotalEntitlements == 0
                    ? 0
                    : (double)usage / lic.TotalEntitlements * 100;

                list.Add(new LicenseUsageSummary
                {
                    LicenseId = lic.Id,
                    ProductName = lic.ProductName,
                    Vendor = lic.Vendor,
                    TotalEntitlements = lic.TotalEntitlements,
                    Assigned = assigned,
                    Installations = installed,
                    UtilizationPercent = utilization
                });
            }

            return list;
        }

        // --------------------------------------------------------
        // 2. COMPLIANCE SUMMARY (High-level counts)
        // --------------------------------------------------------
        public async Task<ComplianceSummary> GetComplianceSummaryAsync()
        {
            var events = await _complianceRepo.GetReportAsync();

            return new ComplianceSummary
            {
                TotalViolations = events.Count,
                Overuse = events.Count(e => e.EventType == "overuse"),
                Underuse = events.Count(e => e.EventType == "underuse"),
                Expiry = events.Count(e => e.EventType == "expiry"),
                Mismatch = events.Count(e => e.EventType == "mismatch"),
                Unused = events.Count(e => e.EventType == "unused")
            };
        }

        // --------------------------------------------------------
        // 3. DASHBOARD REPORT
        // --------------------------------------------------------
        public async Task<DashboardReport> GetDashboardReportAsync()
        {
            return new DashboardReport
            {
                LicenseUtilization = await GetLicenseUsageAsync(),
                Compliance = await GetComplianceSummaryAsync()
            };
        }
    }
}
