using System.ComponentModel.DataAnnotations;

namespace AssetAllocationSystem.BLL.DTOs
{
    public class CreateAssetDto
    {
        [Required]
        public string AssetName { get; set; } = string.Empty;

        [Required]
        public string SerialNumber { get; set; } = string.Empty;
    }
}
