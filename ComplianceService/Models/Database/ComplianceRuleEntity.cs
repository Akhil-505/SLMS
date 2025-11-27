using System.ComponentModel.DataAnnotations;

namespace ComplianceService.Models.Database
{
    public class ComplianceRuleEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";          // e.g., "UnderuseThreshold"

        [Required]
        public string RuleType { get; set; } = "";      // UnderuseThreshold, ExpiryWindowDays, etc.

        [Required]
        public string Value { get; set; } = "";         // threshold or JSON value

        public string Severity { get; set; } = "Low";

        public bool Enabled { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
