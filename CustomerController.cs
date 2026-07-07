using Microsoft.AspNetCore.Mvc;
using MotelBooking.Data;

public class CustomerController : Controller
{


    private readonly ApplicationDbContext _context;

    public CustomerController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Dashboard()
    {
        // Example: get customer name from session
        string? customerName = HttpContext.Session.GetString("CustomerName");

        var bookings = _context.BookingRequests
                               .Where(x => x.CustomerName == customerName)
                               .ToList();

        return View(bookings);

    }

   

       
        


    // Show Register Page
    public IActionResult Register()
    {
        return View();
    }

    // Save Customer
    [HttpPost]
    public IActionResult Register(Customer model)
    {
        if (ModelState.IsValid)
        {
            // TODO: Save to database (later you will add EF or SQL)

            TempData["Success"] = "Registration successful!";
            return RedirectToAction("Register");
        }

        return View(model);
    }



   

}
