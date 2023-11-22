using Microsoft.AspNetCore.Mvc;
using StopSpot.Data;
using StopSpot.Models;

namespace StopSpot.Controllers
{
    public class BookingController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BookingController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(Booking newBooking)
        {
             if (ModelState.IsValid)
             {
                 //BookingModel authenticatedParking = _dbContext.GetBookingModel(Spots newBooking);
                 var authPark = _dbContext.Bookings.FirstOrDefault(park => park.BookingId == newBooking.BookingId);

                 if (authPark != null)//authenticatedParking != null)
                 {

                    _dbContext.Bookings.Add(newBooking);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();

                }

            }
            return NotFound();
        }

    }
}
