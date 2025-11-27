using AuthService.Models.Database;
using AuthService.Repositories;

namespace AuthService.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _repo;

        public RefreshTokenService(IRefreshTokenRepository repo)
        {
            _repo = repo;
        }

        public async Task<RefreshTokenEntity> GenerateRefreshTokenAsync(UserEntity user)
        {
            var token = new RefreshTokenEntity
            {
                UserId = user.Id,
                Token = Guid.NewGuid().ToString(),
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            await _repo.AddAsync(token);
            return token;
        }

        public Task<RefreshTokenEntity?> ValidateRefreshTokenAsync(string token)
        {
            return _repo.GetByTokenAsync(token);
        }

        public async Task RevokeRefreshTokenAsync(RefreshTokenEntity token)
        {
            token.IsRevoked = true;
            await _repo.UpdateAsync(token);
        }
    }
}
