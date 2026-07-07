using System.ComponentModel.DataAnnotations;

namespace MotelBooking.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "Customer";
        // Roles: Admin / Customer

        public string? PhoneNumber { get; set; }
    }
}