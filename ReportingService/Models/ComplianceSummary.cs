namespace ReportingService.Models
{
    public class ComplianceSummary
    {
        public int TotalViolations { get; set; }

        public int Overuse { get; set; }
        public int Underuse { get; set; }
        public int Expiry { get; set; }
        public int Mismatch { get; set; }
        public int Unused { get; set; }
    }
}
