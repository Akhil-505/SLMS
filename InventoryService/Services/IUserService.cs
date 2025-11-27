using InventoryService.Models;

namespace InventoryService.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUserIdAsync(string employeeId);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(int id, User update);
        Task<bool> DeleteAsync(int id);
    }
}
