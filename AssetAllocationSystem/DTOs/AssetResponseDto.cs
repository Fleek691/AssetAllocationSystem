namespace AssetAllocationSystem.DTOs
{
    public class AssetResponseDto
    {
        public int Id { get; set; }
        public string AssetName { get; set; }
        public string SerialNumber { get; set; }
        public bool IsAssigned { get; set; }
    }
}