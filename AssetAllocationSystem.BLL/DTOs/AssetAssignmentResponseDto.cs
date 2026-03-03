namespace AssetAllocationSystem.BLL.DTOs
{
    public class AssetAssignmentResponseDto
    {
        public int Id { get; set; }
        public string AssetName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
