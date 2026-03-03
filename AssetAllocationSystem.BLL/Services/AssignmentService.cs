using AssetAllocationSystem.BLL.DTOs;
using AssetAllocationSystem.BLL.Interfaces;
using AssetAllocationSystem.DAL.Data;
using AssetAllocationSystem.DAL.Entities;
using AssetAllocationSystem.DAL.Interfaces;

namespace AssetAllocationSystem.BLL.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAssetAssignmentRepository _assignmentRepository;
        private readonly ApplicationDbContext _context;

        public AssignmentService(
            IAssetRepository assetRepository,
            IUserRepository userRepository,
            IAssetAssignmentRepository assignmentRepository,
            ApplicationDbContext context)
        {
            _assetRepository = assetRepository;
            _userRepository = userRepository;
            _assignmentRepository = assignmentRepository;
            _context = context;
        }

        public async Task<AssetAssignmentResponseDto> AssignAssetAsync(AssignAssetDto model)
        {
            var asset = await _assetRepository.GetByIdAsync(model.AssetId)
                ?? throw new KeyNotFoundException("Asset not found.");

            if (asset.IsAssigned)
                throw new InvalidOperationException("Asset is already assigned.");

            var user = await _userRepository.GetByIdAsync(model.UserId)
                ?? throw new KeyNotFoundException("User not found.");

            if (user.Role != "Employee")
                throw new InvalidOperationException("Asset can only be assigned to employees.");

            var assignment = new AssetAssignment
            {
                AssetId = asset.Id,
                UserId = user.Id,
                AssignedDate = DateTime.UtcNow
            };

            asset.IsAssigned = true;

            await _assignmentRepository.AddAsync(assignment);
            _assetRepository.Update(asset);
            await _context.SaveChangesAsync();

            return new AssetAssignmentResponseDto
            {
                Id = assignment.Id,
                AssetName = asset.AssetName,
                EmployeeName = user.FullName,
                AssignedDate = assignment.AssignedDate,
                ReturnDate = assignment.ReturnDate
            };
        }

        public async Task<AssetAssignmentResponseDto> ReturnAssetAsync(ReturnAssetDto model)
        {
            var assignment = await _assignmentRepository.GetActiveAssignmentByAssetIdAsync(model.AssetId);

            if (assignment == null)
                throw new KeyNotFoundException("Active assignment not found.");

            assignment.ReturnDate = DateTime.UtcNow;
            assignment.Asset.IsAssigned = false;

            _assignmentRepository.Update(assignment);
            _assetRepository.Update(assignment.Asset);
            await _context.SaveChangesAsync();

            return new AssetAssignmentResponseDto
            {
                Id = assignment.Id,
                AssetName = assignment.Asset.AssetName,
                EmployeeName = assignment.User.FullName,
                AssignedDate = assignment.AssignedDate,
                ReturnDate = assignment.ReturnDate
            };
        }

        public async Task<IEnumerable<AssetAssignmentResponseDto>> GetAssignmentsForUserAsync(int userId)
        {
            var assignments = await _assignmentRepository.GetAssignmentsByUserIdAsync(userId);

            return assignments.Select(a => new AssetAssignmentResponseDto
            {
                Id = a.Id,
                AssetName = a.Asset.AssetName,
                EmployeeName = a.User.FullName,
                AssignedDate = a.AssignedDate,
                ReturnDate = a.ReturnDate
            });
        }
    }
}
