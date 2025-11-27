using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.Database
{
    public class RefreshTokenEntity
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Token { get; set; } = "";

        public DateTime ExpiresAt { get; set; }

        public bool IsRevoked { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
