using AssetAllocationSystem.DAL.Data;
using AssetAllocationSystem.DAL.Entities;
using AssetAllocationSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AssetAllocationSystem.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
        {
            return await _dbSet.Where(u => u.Role == role).ToListAsync();
        }
    }
}
