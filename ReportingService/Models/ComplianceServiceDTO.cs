namespace ReportingService.Models
{
    public class ComplianceResultDto
    {
        public int LicenseId { get; set; }
        public bool IsCompliant { get; set; }
        public string Message { get; set; } = "";
    }

    public class ExpiryAlertDto
    {
        public int LicenseId { get; set; }
        public string LicenseName { get; set; } = "";
        public DateTime ExpiryDate { get; set; }
    }

    public class UnauthorizedInstallDto
    {
        public string DeviceId { get; set; } = "";
        public string ProductName { get; set; } = "";
    }

    public class ComplianceReportDto
    {
        public List<ComplianceResultDto> LicenseCompliance { get; set; } = new();
        public List<ExpiryAlertDto> ExpiringLicenses { get; set; } = new();
        public List<UnauthorizedInstallDto> UnauthorizedInstalls { get; set; } = new();
    }
}
