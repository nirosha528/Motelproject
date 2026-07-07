using System;

namespace MotelBooking.Models
{
    public class Booking
    {
        public int BookingID { get; set; }

        // Customer info
        public string CustomerName { get; set; } = string.Empty;

        public string? CustomerPhone { get; set; }

        // Room details
        public int RoomID { get; set; }

        public string RoomNumber { get; set; } = string.Empty;

        public string RoomType { get; set; } = string.Empty;

        // Booking details
        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public string BookingType { get; set; } = string.Empty;
        // Example: Hourly / Night / Day

        public decimal TotalAmount { get; set; }

        // Status
        public string Status { get; set; } = "Confirmed";

        // 
        public virtual Room? Room { get; set; }
    }
}