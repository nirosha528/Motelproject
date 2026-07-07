using Microsoft.AspNetCore.Mvc;
using MotelBooking.Data;
using MotelBooking.Models;
using System.Linq;

namespace MotelBooking.Controllers
{
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomController(ApplicationDbContext context)
        {
            _context = context;
        }

        // CUSTOMER VIEW
        
        public IActionResult Index()
        {
            return View(_context.Rooms.Where(x => x.IsAvailable).ToList());
        }

        // ADMIN VIEW
        public IActionResult AdminIndex()
        {
            return View(_context.Rooms.ToList());
        }

        
        // CREATE (GET)
       
        public IActionResult Create()
        {
            ViewBag.Rooms = _context.Rooms.ToList();
            return View();
        }

        
        // CREATE (POST)
        
        [HttpPost]
        public IActionResult Create(Room model)
        {
            if (ModelState.IsValid)
            {
                _context.Rooms.Add(model);
                _context.SaveChanges();

                TempData["Success"] = "Room created successfully!";

                return RedirectToAction("Create");
            }

            ViewBag.Rooms = _context.Rooms.ToList();
            return View(model);
        }

        // EDIT (GET)
        
        public IActionResult Edit(int id)
        {
            var room = _context.Rooms.FirstOrDefault(x => x.RoomID == id);
            if (room == null) return NotFound();

            return View(room);
        }

       
        // EDIT (POST)
        
        [HttpPost]
        public IActionResult Edit(Room model)
        {
            var room = _context.Rooms.FirstOrDefault(x => x.RoomID == model.RoomID);

            if (room == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                room.RoomNumber = model.RoomNumber;
                room.RoomType = model.RoomType;
                room.PricePerHourly = model.PricePerHourly;
                room.PricePerNight = model.PricePerNight;
                room.PricePerDay = model.PricePerDay;
                room.IsAvailable = model.IsAvailable;
                room.ImageUrl = model.ImageUrl;

                _context.SaveChanges();

                TempData["Success"] = "Room updated successfully!";

                return RedirectToAction("Create");
            }

            return View(model);
        }

  
        // DELETE (GET)

        public IActionResult Delete(int id)
        {
            var room = _context.Rooms.FirstOrDefault(x => x.RoomID == id);
            if (room == null) return NotFound();

            return View(room);
        }

 
        // DELETE (POST) 
        
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var room = _context.Rooms.FirstOrDefault(x => x.RoomID == id);

            if (room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();

                TempData["Success"] = "Room deleted successfully!";
            }

            return RedirectToAction("Create");
        }
    }
}