namespace ComplianceService.Models
{
    public class LicenseModel
    {
        public int LicenseId { get; set; }
        public string ProductName { get; set; } = "";
        public string Vendor { get; set; } = "";
        public string SKU { get; set; } = "";
        public string LicenseType { get; set; } = "";
        public int TotalEntitlements { get; set; }
        public int Assigned { get; set; }
        public int Available { get; set; }
        public decimal Cost { get; set; }
        public string Currency { get; set; } = "";
        public DateTime? PurchaseDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Notes { get; set; } = "";
    }
}
