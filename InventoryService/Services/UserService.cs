using InventoryService.Models;
using InventoryService.Repositories;

namespace InventoryService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public Task<List<User>> GetAllAsync() => _repo.GetAllAsync();
        public Task<User?> GetByIdAsync(string id) => _repo.GetByIdAsync(id);
        public Task<User?> GetByUserIdAsync(string empId) => _repo.GetByUserIdAsync(empId);

        public async Task<User> CreateAsync(User user)
        {
            await _repo.AddAsync(user);
            return user;
        }

        public async Task<User> UpdateAsync(string id, User update)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) throw new Exception("User not found");

            existing.DisplayName = update.DisplayName;
            existing.Department = update.Department;
            existing.Location = update.Location;

            await _repo.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return false;

            await _repo.DeleteAsync(user);
            return true;
        }
    }
}
