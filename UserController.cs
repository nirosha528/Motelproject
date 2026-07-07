using Microsoft.AspNetCore.Mvc;
using MotelBooking.Data;
using MotelBooking.Models;
using System.Linq;

namespace MotelBooking.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LIST + CREATE PAGE
        public IActionResult Index()
        {
            ViewBag.Users = _context.Users.ToList();
            return View();
        }

        // CREATE USER
        [HttpPost]
        public IActionResult Create(User model)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(model);
                _context.SaveChanges();


                TempData["Success"] = " user add successfully!";
            }

            return RedirectToAction("Index");
        }

        // EDIT GET
        public IActionResult Edit(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserID == id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // EDIT POST
        [HttpPost]
        public IActionResult Edit(User model)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserID == model.UserID);

            if (user != null && ModelState.IsValid)
            {
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.Username = model.Username;
                user.Password = model.Password;
                user.Role = model.Role;
                TempData["Success"] = " user update successfully!";
                _context.SaveChanges();


            }

            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserID == id);

            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                TempData["Success"] = " user delete successfully!";
            }

            return RedirectToAction("Index");
        }
    }
}