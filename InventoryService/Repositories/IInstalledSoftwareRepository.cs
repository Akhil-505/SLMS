using InventoryService.Models;
using System.Linq.Expressions;

namespace InventoryService.Repositories
{
    public interface IInstalledSoftwareRepository
    {
        Task<bool> ExistsAsync(Expression<Func<InstalledSoftware, bool>> predicate);
        Task<List<InstalledSoftware>> GetAllAsync();
        Task<InstalledSoftware?> GetByIdAsync(int id);
        Task AddAsync(InstalledSoftware install);
        Task UpdateAsync(InstalledSoftware install);
        Task DeleteAsync(InstalledSoftware install);

        Task<List<InstalledSoftware>> GetByDeviceIdAsync(int deviceId);
    }
}
