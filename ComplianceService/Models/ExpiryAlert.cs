namespace ComplianceService.Models
{
    public class ExpiryAlert
    {
        public string LicenseName { get; set; } = "";
        public string Vendor { get; set; } = "";
        public DateTime ExpiryDate { get; set; }
        public string Message { get; set; } = "";
        public int LicenseId { get; set; }
    }
}
