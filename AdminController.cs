using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }



    public IActionResult Admin()
    {
        return View();
    }


public IActionResult Analytics()
    {
        return View();
    }
}

