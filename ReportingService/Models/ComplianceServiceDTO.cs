namespace ReportingService.Models
{
    public class ComplianceResultDto
    {
       
            public int Overuse { get; set; }
            public int Underuse { get; set; }
            public int Expiry { get; set; }
            public int Mismatch { get; set; }
            public int Unused { get; set; }
        
    }

    public class ExpiryAlertDto
    {
        public int LicenseId { get; set; }
        public string LicenseName { get; set; } = "";
        public DateTime ExpiryDate { get; set; }
    }

    public class UnauthorizedInstallDto
    {
        public int DeviceId { get; set; } 
        public string ProductName { get; set; } = "";
    }

   
        public class ComplianceReportDto
        {
            public int TotalViolations { get; set; }
            public int Overuse { get; set; }
            public int Underuse { get; set; }
            public int Expiry { get; set; }
            public int Mismatch { get; set; }
            public int Unused { get; set; }
        
    }

}
