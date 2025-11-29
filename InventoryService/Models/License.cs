using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventoryService.Models
{
    public enum LicenseTypeEnum { PerUser, PerDevice, Concurrent, Subscription }

    public class License
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; } = "";

        [Required]
        public string Vendor { get; set; } = "";

        public string SKU { get; set; } = "";

        public LicenseTypeEnum LicenseType { get; set; }
        public int TotalEntitlements { get; set; }

        public decimal Cost { get; set; }
        public string Currency { get; set; } = "USD";

        public DateTime? PurchaseDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public string Notes { get; set; } = "";

        // Navigation
        [JsonIgnore]
        public VendorContract? VendorContract { get; set; }
        [JsonIgnore]
     
        public List<Entitlement> Entitlements { get; set; } = new();
        [JsonIgnore]
        public List<InstalledSoftware> Installations { get; set; } = new();
    }
}
