using AssetAllocationSystem.DAL.Data;
using AssetAllocationSystem.DAL.Entities;
using AssetAllocationSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AssetAllocationSystem.DAL.Repositories
{
    public class AssetAssignmentRepository : Repository<AssetAssignment>, IAssetAssignmentRepository
    {
        public AssetAssignmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<AssetAssignment?> GetActiveAssignmentByAssetIdAsync(int assetId)
        {
            return await _dbSet
                .Include(a => a.Asset)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.AssetId == assetId && a.ReturnDate == null);
        }

        public async Task<IEnumerable<AssetAssignment>> GetAssignmentsByUserIdAsync(int userId)
        {
            return await _dbSet
                .Include(a => a.Asset)
                .Include(a => a.User)
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<AssetAssignment>> GetActiveAssignmentsAsync()
        {
            return await _dbSet
                .Include(a => a.Asset)
                .Include(a => a.User)
                .Where(a => a.ReturnDate == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<AssetAssignment>> GetAssignmentHistoryAsync()
        {
            return await _dbSet
                .Include(a => a.Asset)
                .Include(a => a.User)
                .Where(a => a.ReturnDate != null)
                .ToListAsync();
        }
    }
}
