using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryService.Models
{
    public enum LicenseTypeEnum { PerUser, PerDevice, Concurrent, Subscription }

    public class License
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; } = "";

        [Required]
        public string Vendor { get; set; } = "";

        public string SKU { get; set; } = "";

        public LicenseTypeEnum LicenseType { get; set; }

        // TOTAL seats purchased
        public int TotalEntitlements { get; set; }

        // CURRENT assigned (persisted)
        public int Assigned { get; set; } = 0;

        // Available = TotalEntitlements - Assigned
        [NotMapped]
        public int Available => TotalEntitlements - Assigned;

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
