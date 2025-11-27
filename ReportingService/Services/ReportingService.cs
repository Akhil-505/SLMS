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
        // 2. COMPLIANCE SUMMARY (High level counts)
        // --------------------------------------------------------
        public async Task<ComplianceSummary> GetComplianceSummaryAsync()
        {
            var report = await _complianceRepo.GetReportAsync();

            if (report == null)
                return new ComplianceSummary();

            int compliant = report.LicenseCompliance.Count(l => l.IsCompliant);
            int nonCompliant = report.LicenseCompliance.Count(l => !l.IsCompliant);

            return new ComplianceSummary
            {
                TotalLicenses = report.LicenseCompliance.Count,
                CompliantLicenses = compliant,
                NonCompliantLicenses = nonCompliant,
                TotalUnauthorizedInstalls = report.UnauthorizedInstalls.Count,
                TotalExpiringSoon = report.ExpiringLicenses.Count
            };
        }

        // --------------------------------------------------------
        // 3. DASHBOARD REPORT (Main API for UI)
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
