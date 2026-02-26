using AssetAllocationSystem.Data;
using AssetAllocationSystem.DTOs;
using AssetAllocationSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AssetAllocationSystem.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly ApplicationDbContext _context;

        public AssignmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AssetAssignmentResponseDto> AssignAssetAsync(AssignAssetDto model)
        {
            var asset = await _context.Assets.FindAsync(model.AssetId)
                ?? throw new KeyNotFoundException("Asset not found.");

            if (asset.IsAssigned)
                throw new InvalidOperationException("Asset is already assigned.");

            var user = await _context.Users.FindAsync(model.UserId)
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

            _context.AssetAssignments.Add(assignment);
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
            var assignment = await _context.AssetAssignments
                .Include(a => a.Asset)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a =>
                    a.AssetId == model.AssetId &&
                    a.ReturnDate == null);

            if (assignment == null)
                throw new KeyNotFoundException("Active assignment not found.");

            assignment.ReturnDate = DateTime.UtcNow;
            assignment.Asset.IsAssigned = false;

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
            return await _context.AssetAssignments
                .Include(a => a.Asset)
                .Include(a => a.User)
                .Where(a => a.UserId == userId)
                .Select(a => new AssetAssignmentResponseDto
                {
                    Id = a.Id,
                    AssetName = a.Asset.AssetName,
                    EmployeeName = a.User.FullName,
                    AssignedDate = a.AssignedDate,
                    ReturnDate = a.ReturnDate
                })
                .ToListAsync();
        }
    }
}