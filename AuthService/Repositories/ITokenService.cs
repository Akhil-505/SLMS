using AuthService.Models.Database;

namespace AuthService.Services
{
    public interface ITokenService
    {
        string GenerateToken(UserEntity user);
    }
}
