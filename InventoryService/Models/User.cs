using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventoryService.Models
{
    public class User
    {
       


        public string UserId { get; set; } = ""; // employeeId or email

        public string DisplayName { get; set; } = "";
        public string Department { get; set; } = "";
        public string Location { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Nav
        [JsonIgnore]
        public List<Device> Devices { get; set; } = new();
        [JsonIgnore]
        public List<Entitlement> Entitlements { get; set; } = new();
    }
}
