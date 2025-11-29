using InventoryService.Models;

namespace InventoryService.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(string id);
        Task<User?> GetByUserIdAsync(string employeeId);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(string id, User update);
        Task<bool> DeleteAsync(string id);
    }
}
