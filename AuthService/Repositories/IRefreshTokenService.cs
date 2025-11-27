using AuthService.Models.Database;

namespace AuthService.Services
{
    public interface IRefreshTokenService
    {
        Task<RefreshTokenEntity> GenerateRefreshTokenAsync(UserEntity user);
        Task<RefreshTokenEntity?> ValidateRefreshTokenAsync(string token);
        Task RevokeRefreshTokenAsync(RefreshTokenEntity token);
    }
}
