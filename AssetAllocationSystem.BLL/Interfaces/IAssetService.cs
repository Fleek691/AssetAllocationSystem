using AssetAllocationSystem.BLL.DTOs;

namespace AssetAllocationSystem.BLL.Interfaces
{
    public interface IAssetService
    {
        Task<AssetResponseDto> CreateAssetAsync(CreateAssetDto model);
        Task<AssetResponseDto> UpdateAssetAsync(int id, UpdateAssetDto model);
        Task<bool> DeleteAssetAsync(int id);
        Task<IEnumerable<AssetResponseDto>> GetAllAssetsAsync();
    }
}
