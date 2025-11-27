using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComplianceService.Models.Database
{
    public class ComplianceEventEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EventType { get; set; } = "";   // Overuse, Underuse, Expiry, Unauthorized

        public string Severity { get; set; } = "Medium";

        [Required]
        public string Details { get; set; } = "";     // Human-readable description

        public int? LicenseId { get; set; }
        public string? DeviceId { get; set; }
        public string? UserId { get; set; }

        public bool Resolved { get; set; } = false;

        public string? ResolvedBy { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public string? ResolutionNote { get; set; }

        public string CreatedBy { get; set; } = "System";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Additional JSON payload
        [Column(TypeName = "nvarchar(max)")]
        public string? MetadataJson { get; set; }
    }
}
