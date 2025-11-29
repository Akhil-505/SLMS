using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryService.Models
{
    public class Entitlement
    {
        [Key]
        public int Id { get; set; }

        // FK to License
        public int LicenseId { get; set; }
        [JsonIgnore]
        public License? License { get; set; }

        // assignment target: either user or device (or both nullable)
        public string? UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

        public int? DeviceId { get; set; }
        [JsonIgnore]
        public Device? Device { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }

       
    }
}
