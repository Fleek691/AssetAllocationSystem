using AssetAllocationSystem.BLL.DTOs;

namespace AssetAllocationSystem.BLL.Interfaces
{
    public interface IAssignmentService
    {
        Task<AssetAssignmentResponseDto> AssignAssetAsync(AssignAssetDto model);
        Task<AssetAssignmentResponseDto> ReturnAssetAsync(ReturnAssetDto model);
        Task<IEnumerable<AssetAssignmentResponseDto>> GetAssignmentsForUserAsync(int userId);
    }
}
