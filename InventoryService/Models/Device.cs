using System.ComponentModel.DataAnnotations;

namespace InventoryService.Models
{
    public class Device
    {
        public int Id { get; set; }

        [Required]
        public string DeviceId { get; set; } = "";

        public string Hostname { get; set; } = "";
        public int? OwnerUserId { get; set; }

        // Navigation
        public User? OwnerUser { get; set; }

        public string Department { get; set; } = "";
        public string Location { get; set; } = "";
        public DateTime LastSeen { get; set; } = DateTime.UtcNow;

        public List<InstalledSoftware> InstalledSoftware { get; set; } = new();
    }
}
