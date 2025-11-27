namespace ReportingService.Models
{
    public class LicenseUsageSummary
    {
        public int LicenseId { get; set; }
        public string ProductName { get; set; } = "";
        public string Vendor { get; set; } = "";
        public int TotalEntitlements { get; set; }
        public int Assigned { get; set; }
        public int Installations { get; set; }
        public double UtilizationPercent { get; set; }
    }
}
