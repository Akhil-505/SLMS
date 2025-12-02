using System.Text.Json.Serialization;

namespace InventoryService.Models
{
    public class InstalledSoftware
    {
        public int Id { get; set; }
        public int InstalledSoftwareId { get; set; }
        public int DeviceId { get; set; }
        [JsonIgnore]
        public Device? Device { get; set; }


        public int? LicenseId { get; set; }
        [JsonIgnore]
        public License? License { get; set; }

        public string ProductName { get; set; } = "";
        public string Version { get; set; } = "";
        public DateTime InstallDate { get; set; } = DateTime.UtcNow;


        [JsonIgnore]

        public List<InstallationHistory> History { get; set; } = new();
    }
}
