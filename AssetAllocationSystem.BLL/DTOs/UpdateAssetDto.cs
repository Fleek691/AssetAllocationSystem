using System.ComponentModel.DataAnnotations;

namespace AssetAllocationSystem.BLL.DTOs
{
    public class UpdateAssetDto
    {
        [Required]
        public string AssetName { get; set; }
    }
}
