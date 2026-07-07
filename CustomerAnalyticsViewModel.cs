using System.Collections.Generic;

namespace MotelBooking.Models
{
    public class CustomerAnalyticsViewModel
    {
        public List<MonthlyBooking> MonthlyBookings { get; set; } = new();

        public string PopularRoom { get; set; } = string.Empty;

        public int PeakHour { get; set; }

        public List<CustomerGrowth> CustomerGrowth { get; set; } = new();
    }

    public class MonthlyBooking
    {
        public string Month { get; set; } = string.Empty;
        public int Total { get; set; }
    }

    public class CustomerGrowth
    {
        public string Month { get; set; } = string.Empty;
        public int Customers { get; set; }
    }
}