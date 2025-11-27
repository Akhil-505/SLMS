namespace ReportingService.Models
{
    public class ComplianceSummary
    {
        public int TotalLicenses { get; set; }
        public int CompliantLicenses { get; set; }
        public int NonCompliantLicenses { get; set; }
        public int TotalUnauthorizedInstalls { get; set; }
        public int TotalExpiringSoon { get; set; }
    }
}
