using InventoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db) => _db = db;

        public Task<List<User>> GetAllAsync() =>
            _db.Users.Include(u => u.Devices).Include(u => u.Entitlements).ToListAsync();

        public Task<User?> GetByIdAsync(int id) =>
            _db.Users
               .Include(u => u.Devices)
               .Include(u => u.Entitlements)
               .ThenInclude(e => e.License)
               .FirstOrDefaultAsync(u => u.Id == id);

        public Task<User?> GetByUserIdAsync(string userId) =>
            _db.Users
               .Include(u => u.Devices)
               .Include(u => u.Entitlements)
               .ThenInclude(e => e.License)
               .FirstOrDefaultAsync(u => u.UserId == userId);

        public async Task AddAsync(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }
    }
}
