using AssetAllocationSystem.DAL.Entities;

namespace AssetAllocationSystem.DAL.Interfaces
{
    public interface IAssetRepository : IRepository<Asset>
    {
        Task<Asset?> GetBySerialNumberAsync(string serialNumber);
        Task<IEnumerable<Asset>> GetUnassignedAssetsAsync();
        Task<IEnumerable<Asset>> GetAssignedAssetsAsync();
    }
}
