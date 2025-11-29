using System.Text.Json.Serialization;

namespace InventoryService.Models
{
    public class VendorContract
    {
        public int Id { get; set; }

        public int LicenseId { get; set; }
        [JsonIgnore]
        public License? License { get; set; }

        public string VendorName { get; set; } = "";
        public string ContractNumber { get; set; } = "";
        public DateTime PurchaseDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Terms { get; set; } = "";
        public decimal Price { get; set; }
    }
}
