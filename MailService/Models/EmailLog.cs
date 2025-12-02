namespace MailService.Models
{
    public class EmailLog
    {
        public int Id { get; set; }
        public string To { get; set; } = "";
        public string Subject { get; set; } = "";
        public bool Success { get; set; }
        public DateTime SentAt { get; set; }
    }
}
