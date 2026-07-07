using System.ComponentModel.DataAnnotations;

public class Customer
{
    public int CustomerID { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [Phone]
    [StringLength(15)]
    public string PhoneNumber { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; }

    [Required]
    [StringLength(100)]
    public string Password { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public string Role { get; set; } = "Customer";
}