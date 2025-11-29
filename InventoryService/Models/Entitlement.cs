using System.Text.Json.Serialization;

namespace InventoryService.Models
{
    public class Entitlement
    {
        public int Id { get; set; }

        public int LicenseId { get; set; }
        [JsonIgnore]
        public License? License { get; set; }

        public int? UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

        public int? DeviceId { get; set; }
        [JsonIgnore]
        public Device? Device { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }
    }
}
