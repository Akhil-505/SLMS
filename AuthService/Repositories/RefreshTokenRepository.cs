using AuthService.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AuthDbContext _db;

        public RefreshTokenRepository(AuthDbContext db)
        {
            _db = db;
        }

        public Task<RefreshTokenEntity?> GetByTokenAsync(string token)
        {
            return _db.RefreshTokens
                .FirstOrDefaultAsync(r => r.Token == token && !r.IsRevoked);
        }

        public Task<List<RefreshTokenEntity>> GetTokensByUserAsync(int userId)
        {
            return _db.RefreshTokens
                .Where(r => r.UserId == userId && !r.IsRevoked)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task AddAsync(RefreshTokenEntity refreshToken)
        {
            await _db.RefreshTokens.AddAsync(refreshToken);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(RefreshTokenEntity refreshToken)
        {
            _db.RefreshTokens.Update(refreshToken);
            await _db.SaveChangesAsync();
        }
    }
}
