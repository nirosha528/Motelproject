using System;
using System.ComponentModel.DataAnnotations;

namespace MotelBooking.Models
{
    public class BookingRequest
    {
        [Key]
        public int RequestID { get; set; }

        [Required(ErrorMessage = "Customer name is required")]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        public int RoomNo { get; set; }

        [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime CheckOut { get; set; }

        [Required(ErrorMessage = "Room Type is required")]
        public string RoomType { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";
    }
}