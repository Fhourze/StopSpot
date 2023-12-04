﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            ViewBag.spots = _dbContext.ParkingLists.ToList();
            ViewBag.spotsPrice = _dbContext.ParkingLists.Select(column => column.PricePerHour).ToList();
            ViewBag.spotsPicture = _dbContext.ParkingLists.Select(column => column.Picture).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Index(Booking newBooking)
        {
            if (ModelState.IsValid)
            {
                var authPark = _dbContext.Bookings.FirstOrDefault(park => park.BookingId == newBooking.BookingId);

                if (authPark == null)
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

        [HttpGet]
        public IActionResult spotNumCont(int spotNo)
        {
            var authSpotNum = _dbContext.ParkingLists.FirstOrDefault(park => park.Id == spotNo);
            ViewBag.CSpotNum = authSpotNum.PricePerHour;

            return RedirectToAction("Index");
        }


    }
}
