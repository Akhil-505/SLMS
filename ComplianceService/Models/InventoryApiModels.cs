namespace ComplianceService.Models
{
    public class LicenseDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = "";
        public string Vendor { get; set; } = "";
        public int TotalEntitlements { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public VendorContractDto? VendorContract { get; set; }
    }

    public class VendorContractDto
    {
        public DateTime? ExpiryDate { get; set; }
    }

    public class EntitlementDto
    {
        public int LicenseId { get; set; }
    }

    public class InstalledSoftwareDto
    {
        public string ProductName { get; set; } = "";
        public int? LicenseId { get; set; }
        public string DeviceId { get; set; } = "";
        public string Version { get; set; } = "";
    }

    public class SoftwareCatalogDto
    {
        public string ProductName { get; set; } = "";
        public string Vendor { get; set; } = "";
    }
}
