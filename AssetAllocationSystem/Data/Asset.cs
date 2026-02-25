using System.ComponentModel.DataAnnotations;

namespace AssetAllocationSystem.Data
{
    public class Asset
    {
        public int Id { get; set; }

        [Required]
        public string AssetName { get; set; }

        [Required]
        public string SerialNumber { get; set; }

        public bool IsAssigned { get; set; } = false;

        // Navigation
        public AssetAssignment? AssetAssignment { get; set; }
    }
}