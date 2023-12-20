using Microsoft.AspNetCore.Mvc;
using StopSpot.Models;
using System.Diagnostics;

namespace StopSpot.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: Index
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var accountTypeClaim = User.FindFirst("AccountType")?.Value;
                if (accountTypeClaim != null)
                {
                    if (accountTypeClaim == "Driver")
                    {
                        return RedirectToAction("IndexDriver");
                    }
                }
            }

            // Default redirect if not a Driver or other conditions
            return View();
        }

        // Additional action for Driver
        public IActionResult IndexDriver()
        {
            // Logic for Driver view
            return View("IndexDriver"); // Replace "IndexDriver" with the actual name of the Driver view
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult BookParking()
        {
            return View();
        }

        public IActionResult ListingModel()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}