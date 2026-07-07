using Microsoft.AspNetCore.Mvc;
using MotelBooking.Data;
using MotelBooking.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MotelBooking.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        // LOGIN PAGE
     
        public IActionResult Login()
        {
            return View();
        }

       
        // LOGIN PROCESS
        //
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _context.Users.FirstOrDefault(u =>
                u.Username == model.Username &&
                u.Password == model.Password &&
                u.Role == model.Role);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role);

                if (user.Role == "Admin")
                {
                    HttpContext.Session.SetString("AdminLoggedIn", "true");

                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    HttpContext.Session.SetString("CustomerLoggedIn", "true");

                    // go to Customer Dashboard
                    TempData["Success"] = "Login successful!";
                   
                    return RedirectToAction("Dashboard", "Customer");
                }
            }

            ViewBag.Error = "Invalid username or password";
            return View(model);
        }

        


        
            // REGISTER PAGE
            public IActionResult Register()
            {
                return View();
            }

            // REGISTER POST
            [HttpPost]
            public IActionResult Register(User model)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                model.Role = "Customer"; 

                _context.Users.Add(model);
                _context.SaveChanges();

                TempData["Success"] = "Registration successful!";
                return RedirectToAction("Login");
            }

            
   

    
    // LOGOUT
    
    public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}