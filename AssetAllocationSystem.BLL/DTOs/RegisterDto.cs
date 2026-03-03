using System.ComponentModel.DataAnnotations;

namespace AssetAllocationSystem.BLL.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // Admin or Employee
    }
}
