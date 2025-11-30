public class ComplianceEvent
{
    public int Id { get; set; }
    public int LicenseId { get; set; }

    public string ProductName { get; set; } = "";
    public string EventType { get; set; } = "";
    public string Severity { get; set; } = "";

    public string Details { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool Resolved { get; set; }
    public string? ResolutionNote { get; set; }
}
