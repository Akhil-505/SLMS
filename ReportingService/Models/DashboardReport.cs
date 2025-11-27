namespace ReportingService.Models
{
    public class DashboardReport
    {
        public List<LicenseUsageSummary> LicenseUtilization { get; set; } = new();
        public ComplianceSummary Compliance { get; set; } = new();
    }
}
