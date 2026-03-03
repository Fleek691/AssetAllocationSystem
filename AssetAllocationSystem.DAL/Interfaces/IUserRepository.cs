using AssetAllocationSystem.DAL.Entities;

namespace AssetAllocationSystem.DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetUsersByRoleAsync(string role);
    }
}
