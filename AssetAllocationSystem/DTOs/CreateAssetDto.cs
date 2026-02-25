using System.ComponentModel.DataAnnotations;

namespace AssetAllocationSystem.DTOs
{
    public class CreateAssetDto
    {
        [Required]
        public string AssetName { get; set; }

        [Required]
        public string SerialNumber { get; set; }
    }
}