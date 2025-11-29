using InventoryService.Models;

namespace InventoryService.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(string id);
        Task<User?> GetByUserIdAsync(string userId);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
