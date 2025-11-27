namespace ComplianceService.Models
{
    public class ComplianceResult
    {
        public string LicenseName { get; set; } = "";
        public string Vendor { get; set; } = "";
        public int TotalEntitlements { get; set; }
        public int AssignedEntitlements { get; set; }
        public int TotalInstallations { get; set; }
        public bool IsCompliant { get; set; }
        public string Message { get; set; } = "";
        public int LicenseId { get; set; }
    }
}
