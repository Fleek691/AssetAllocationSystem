using AssetAllocationSystem.DTOs;
using AssetAllocationSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AssetAllocationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        // 🔹 Admin assigns asset to employee
        [Authorize(Roles = "Admin")]
        [HttpPost("assign")]
        public async Task<IActionResult> Assign(AssignAssetDto model)
        {
            var result = await _assignmentService.AssignAssetAsync(model);
            return Ok(result);
        }

        // 🔹 Admin marks asset as returned
        [Authorize(Roles = "Admin")]
        [HttpPost("return")]
        public async Task<IActionResult> Return(ReturnAssetDto model)
        {
            var result = await _assignmentService.ReturnAssetAsync(model);
            return Ok(result);
        }

        // 🔹 Employee views their own assignments
        [Authorize(Roles = "Employee")]
        [HttpGet("my-assets")]
        public async Task<IActionResult> GetMyAssets()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _assignmentService.GetAssignmentsForUserAsync(userId);
            return Ok(result);
        }
    }
}