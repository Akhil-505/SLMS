using InventoryService.Models;

namespace InventoryService.Models
{
    public class LicenseDto
    {
        public int LicenseId { get; set; }
        public string ProductName { get; set; } = "";
        public string Vendor { get; set; } = "";
        public string LicenseType { get; set; } = "";
        public int TotalEntitlements { get; set; }
        public int Assigned { get; set; }
        public int Available { get; set; }
        public string SKU { get; set; } = "";
        public DateTime? PurchaseDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal Cost { get; set; }
        public string Currency { get; set; } = "";
        public string Notes { get; set; } = "";

        public LicenseDto(License l)
        {
            LicenseId = l.Id;
            ProductName = l.ProductName;
            Vendor = l.Vendor;
            LicenseType = l.LicenseType.ToString().ToLower();
            TotalEntitlements = l.TotalEntitlements;
            Assigned = l.Assigned;
            Available = l.Available;
            SKU = l.SKU;
            PurchaseDate = l.PurchaseDate;
            ExpiryDate = l.ExpiryDate;
            Cost = l.Cost;
            Currency = l.Currency;
            Notes = l.Notes;
        }
    }
}
