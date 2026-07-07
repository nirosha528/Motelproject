using System.ComponentModel.DataAnnotations;

namespace MotelBooking.Models
{
    public class Room
    {
        public int RoomID { get; set; }

        [Required]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        public string RoomType { get; set; } = string.Empty;

        public decimal PricePerHourly { get; set; }
        public decimal PricePerNight { get; set; }
        public decimal PricePerDay { get; set; }

        public bool IsAvailable { get; set; }

        public string? ImageUrl { get; set; }
    }
}