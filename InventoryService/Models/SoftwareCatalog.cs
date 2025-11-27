using System.ComponentModel.DataAnnotations;

namespace InventoryService.Models
{
    public enum SoftwareStatus { Active, Retired }

    public class SoftwareCatalog
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; } = "";

        [Required]
        public string Vendor { get; set; } = "";

        public string Category { get; set; } = "";
        public string Description { get; set; } = "";
        public string SKU { get; set; } = "";

        public SoftwareStatus Status { get; set; } = SoftwareStatus.Active;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
