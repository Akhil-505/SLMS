using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventoryService.Models
{
    public class Device
    {
        public int Id { get; set; }

        [Required]
        public string DeviceId { get; set; } = "";

        public string Hostname { get; set; } = "";
        public string? OwnerUserId { get; set; }

        // Navigation
        [JsonIgnore]
        public User? OwnerUser { get; set; }

        public string Department { get; set; } = "";
        public string Location { get; set; } = "";
        public DateTime LastSeen { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public List<InstalledSoftware> InstalledSoftware { get; set; } = new();
    }
}
