using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FrontEndService.Models;
using Microsoft.AspNetCore.Authorization;

namespace FrontEndService.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Authorize]
    public IActionResult Login()
    {
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Logout()
    {
        return SignOut("Cookies", "iodc");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
