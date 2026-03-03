using AssetAllocationSystem.DAL.Data;
using AssetAllocationSystem.DAL.Entities;
using AssetAllocationSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AssetAllocationSystem.DAL.Repositories
{
    public class AssetRepository : Repository<Asset>, IAssetRepository
    {
        public AssetRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Asset?> GetBySerialNumberAsync(string serialNumber)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.SerialNumber == serialNumber);
        }

        public async Task<IEnumerable<Asset>> GetUnassignedAssetsAsync()
        {
            return await _dbSet.Where(a => !a.IsAssigned).ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetAssignedAssetsAsync()
        {
            return await _dbSet.Where(a => a.IsAssigned).ToListAsync();
        }
    }
}
