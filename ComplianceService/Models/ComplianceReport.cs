namespace ComplianceService.Models
{
    public class ComplianceReport
    {
        public List<ComplianceResult> LicenseCompliance { get; set; } = new();
        public List<ExpiryAlert> ExpiringLicenses { get; set; } = new();
        public List<UnauthorizedInstall> UnauthorizedInstalls { get; set; } = new();
    }
}
