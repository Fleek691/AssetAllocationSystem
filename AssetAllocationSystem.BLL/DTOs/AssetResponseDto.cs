namespace AssetAllocationSystem.BLL.DTOs
{
    public class AssetResponseDto
    {
        public int Id { get; set; }
        public string AssetName { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public bool IsAssigned { get; set; }
    }
}
