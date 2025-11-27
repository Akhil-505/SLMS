using AuthService.Models.Database;

namespace AuthService.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenEntity?> GetByTokenAsync(string token);
        Task<List<RefreshTokenEntity>> GetTokensByUserAsync(int userId);
        Task AddAsync(RefreshTokenEntity refreshToken);
        Task UpdateAsync(RefreshTokenEntity refreshToken);
    }
}
