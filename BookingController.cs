using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MotelBooking.Data;
using MotelBooking.Models;
using System.Linq;

namespace MotelBooking.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        // VIEW ALL BOOKINGS
        // 
        public IActionResult Index()
        {
            var bookings = _context.Bookings.ToList();
            return View(bookings);
        }


        // CREATE BOOKING (GET)

        //public IActionResult Create()
        //{
        //    return View();
        // }

        // GET: Booking/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Booking model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Find room using room number entered by user
            var room = _context.Rooms
                .FirstOrDefault(r => r.RoomNumber == model.RoomNumber);

            if (room == null)
            {
                ModelState.AddModelError("RoomNumber", "Room number not found.");
                return View(model);
            }

            // Set foreign key and room details
            model.RoomID = room.RoomID;
            model.RoomType = room.RoomType;
            model.Status = "Confirmed";

            _context.Bookings.Add(model);
            _context.SaveChanges();

            TempData["Success"] = "Booking created successfully!";

            return RedirectToAction("Index");
        }


        // CONVERT REQUEST TO BOOKING

        public IActionResult CreateFromRequest(int id)
        {
            var request = _context.BookingRequests
                                  .FirstOrDefault(x => x.RequestID == id);

            if (request == null)
            {
                return RedirectToAction("AdminRequests", "BookingRequest");
            }

            var booking = new Booking
            {
                CustomerName = request.CustomerName,
                RoomNumber = request.RoomNo.ToString(),
                RoomType = request.RoomType,
                CheckIn = request.CheckIn,
                CheckOut = request.CheckOut,
                BookingType = "Day",
                Status = "Confirmed"
            };

            _context.Bookings.Add(booking);

            request.Status = "Approved";

            _context.SaveChanges();

            TempData["Success"] = "Request approved and booking created!";

            return RedirectToAction("Index");
        }

      
        // =========================
    // EDIT (GET)
    // =========================
    public IActionResult Edit(int id)
        {
            var booking = _context.Bookings
                .FirstOrDefault(x => x.BookingID == id);

            if (booking == null)
                return NotFound();

            ViewBag.Rooms = new SelectList(
                _context.Rooms,
                "RoomID",
                "RoomNumber",
                booking.RoomID);

            return View(booking);
        }

        // =========================
        // EDIT (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Booking model)
        {
            var booking = _context.Bookings
                .FirstOrDefault(x => x.BookingID == model.BookingID);

            if (booking == null)
                return NotFound();

            var room = _context.Rooms
                .FirstOrDefault(r => r.RoomID == model.RoomID);

            if (room == null)
            {
                ModelState.AddModelError("RoomID", "Please select a valid room.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Rooms = new SelectList(
                    _context.Rooms,
                    "RoomID",
                    "RoomNumber",
                    model.RoomID);

                return View(model);
            }

            booking.CustomerName = model.CustomerName;
            booking.CustomerPhone = model.CustomerPhone;

            booking.RoomID = room.RoomID;
            booking.RoomNumber = room.RoomNumber;
            booking.RoomType = room.RoomType;

            booking.CheckIn = model.CheckIn;
            booking.CheckOut = model.CheckOut;

            booking.BookingType = model.BookingType;
            booking.TotalAmount = model.TotalAmount;
            booking.Status = model.Status;

            _context.SaveChanges();

            TempData["Success"] = "Booking updated successfully!";
            return RedirectToAction("Index");
        }

        // DELETE

        public IActionResult Delete(int id)
        {
            var booking = _context.Bookings
                                  .FirstOrDefault(x => x.BookingID == id);

            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();

                TempData["Success"] = "Booking deleted successfully!";
            }

            return RedirectToAction("Index");
        }

        
        // DETAILS
       
        public IActionResult Details(int id)
        {
            var booking = _context.Bookings
                                  .FirstOrDefault(x => x.BookingID == id);

            if (booking == null)
                return NotFound();

            return View(booking);
        }
    }
}