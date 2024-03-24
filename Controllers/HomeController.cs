// Kod tillhörande lösningen till projektuppgiften i kurs DT191G, skapat av Cecilia Edvardsson

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hairsalon.Models;

using Microsoft.AspNetCore.Authorization;

namespace Hairsalon.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // Startsida för admin
    // Home/Index
    [Authorize]
    [Route("/admin")]
    public IActionResult Index()
    {
        return View();
    }

    // Publik startsida
    // Home/ClientIndex
    [Route("/")]
    public IActionResult ClientIndex()
    {
        return View();
    }

    // Publik kontakta-oss-sida
    // Home/Contact
    [Route("/kontaktaoss")]
    public IActionResult ClientContact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
