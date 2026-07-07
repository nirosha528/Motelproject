using Microsoft.AspNetCore.Mvc;
using MotelBooking.Data;
using MotelBooking.Models;
using System.Linq;

namespace MotelBooking.Controllers
{
    public class BookingRequestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingRequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        // CUSTOMER PAGE (FORM + LIST)
      
        public IActionResult Create()
        {
            ViewBag.Bookings = _context.BookingRequests.ToList();
            return View();
        }

        // SAVE BOOKING
        [HttpPost]
        public IActionResult Create(BookingRequest model)
        {
            if (ModelState.IsValid)
            {
                model.Status = "Pending";

                _context.BookingRequests.Add(model);
                _context.SaveChanges();

                TempData["Success"] = "Booking request created successfully!";
                return RedirectToAction("Create");
            }

            ViewBag.Bookings = _context.BookingRequests.ToList();
            return View(model);
        }


        public IActionResult Edit(int id)
        {
            var data = _context.BookingRequests.FirstOrDefault(x => x.RequestID == id);

            if (data == null)
                return NotFound();

            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(BookingRequest model)
        {
            var data = _context.BookingRequests.FirstOrDefault(x => x.RequestID == model.RequestID);

            if (data == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                data.CustomerName = model.CustomerName;
                data.RoomNo = model.RoomNo;
                data.RoomType = model.RoomType;
                data.CheckIn = model.CheckIn;
                data.CheckOut = model.CheckOut;
                data.Status = model.Status;

                _context.SaveChanges();

                TempData["Success"] = "Booking request updated successfully!";
            }

            return RedirectToAction("AdminRequest");
        }

        // ADMIN PAGE
     
        public IActionResult AdminRequest()
        {
            var data = _context.BookingRequests.ToList();
            return View(data);
        }
    

    // =========================
    // DELETE (GET)
    // =========================
    public IActionResult Delete(int id)
        {
            var request = _context.BookingRequests
                .FirstOrDefault(x => x.RequestID == id);

            if (request == null)
                return NotFound();

            return View(request);
        }

        // =========================
        // DELETE (POST)
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var request = _context.BookingRequests
                .FirstOrDefault(x => x.RequestID == id);

            if (request == null)
                return NotFound();

            _context.BookingRequests.Remove(request);
            _context.SaveChanges();

            TempData["Success"] = "Booking request deleted successfully!";
            return RedirectToAction("AdminRequest");
        }
    }
}