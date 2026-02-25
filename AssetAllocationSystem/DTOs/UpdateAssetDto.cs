using System.ComponentModel.DataAnnotations;

namespace AssetAllocationSystem.DTOs
{
    public class UpdateAssetDto
    {
        [Required]
        public string AssetName { get; set; }
    }
}