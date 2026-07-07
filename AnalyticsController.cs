using Microsoft.AspNetCore.Mvc;
using MotelBooking.Data;
using MotelBooking.Models;
using System;
using System.Globalization;
using System.Linq;

namespace MotelBooking.Controllers
{
    public class AnalyticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnalyticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult CustomerDashboard()
        {
            var bookings = _context.BookingRequests.ToList();

            // Monthly bookings
            var monthly = bookings
                .GroupBy(x => x.CheckIn.Month)
                .Select(g => new MonthlyBooking
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),
                    Total = g.Count()
                }).ToList();

            var popularRoom = bookings
    .GroupBy(x => x.RoomNo)
    .OrderByDescending(g => g.Count())
    .FirstOrDefault()?.Key.ToString() ?? "N/A";

            // Peak booking hour
            var peakHour = bookings
                .GroupBy(x => x.CheckIn.Hour)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? 0;

            //  Customer growth (by month)
            var growth = bookings
                .GroupBy(x => new { x.CheckIn.Year, x.CheckIn.Month })
                .OrderBy(x => x.Key.Year).ThenBy(x => x.Key.Month)
                .Select(g => new CustomerGrowth
                {
                    Month = $"{g.Key.Month}/{g.Key.Year}",
                    Customers = g.Select(x => x.CustomerName).Distinct().Count()
                }).ToList();

            var model = new CustomerAnalyticsViewModel
            {
                MonthlyBookings = monthly,
                PopularRoom = popularRoom,
                PeakHour = peakHour,
                CustomerGrowth = growth
            };

            return View(model);
        }
    }
}