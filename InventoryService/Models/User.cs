using System.ComponentModel.DataAnnotations;

namespace InventoryService.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = ""; // employeeId or email

        public string DisplayName { get; set; } = "";
        public string Department { get; set; } = "";
        public string Location { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Nav
        public List<Device> Devices { get; set; } = new();
        public List<Entitlement> Entitlements { get; set; } = new();
    }
}
