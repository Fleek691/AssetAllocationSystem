using AssetAllocationSystem.DAL.Entities;

namespace AssetAllocationSystem.DAL.Interfaces
{
    public interface IAssetAssignmentRepository : IRepository<AssetAssignment>
    {
        Task<AssetAssignment?> GetActiveAssignmentByAssetIdAsync(int assetId);
        Task<IEnumerable<AssetAssignment>> GetAssignmentsByUserIdAsync(int userId);
        Task<IEnumerable<AssetAssignment>> GetActiveAssignmentsAsync();
        Task<IEnumerable<AssetAssignment>> GetAssignmentHistoryAsync();
    }
}
