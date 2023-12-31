﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StopSpot.Data;
using StopSpot.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
namespace StopSpot.Controllers
{
    public class BookingController : Controller
    {
        private readonly AppDbContext _dbContext;
        //public int i;

        public BookingController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.spots = _dbContext.ParkingLists.Where(a => a.Availability == "True").ToList();
            ViewBag.spotsPrice = _dbContext.ParkingLists.Where(a => a.Availability == "True").Select(column => column.PricePerHour).ToList();
            ViewBag.spotsPicture = _dbContext.ParkingLists.Where(a => a.Availability == "True").Select(column => column.Picture).ToList();
            ViewBag.spotsAddress = _dbContext.ParkingLists.Where(a => a.Availability == "True").Select(column => column.Address).ToList();
            ViewBag.bookFrom = _dbContext.Bookings.Select(column => column.ParkingFrom).ToList();
            ViewBag.bookUntil = _dbContext.Bookings.Select(column => column.ParkingUntil).ToList();
            ViewBag.bookedSpots = _dbContext.Bookings.Select(column => column.ParkingSpot).ToList();

            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            ViewBag.userid = claims.FirstOrDefault().ToString();

            return View();
        }

        [HttpPost]
        public IActionResult Index(Booking newBooking)
        {
            if (ModelState.IsValid)
            {
                //BookingModel authenticatedParking = _dbContext.GetBookingModel(Spots newBooking);
                var authPark = _dbContext.Bookings.FirstOrDefault(park => park.BookingId == newBooking.BookingId);
                var parkFromFinal = newBooking.ParkingFrom;
                var parkUntilFinal = newBooking.ParkingUntil;

                var isBefore = DateTime.Compare(parkFromFinal, parkUntilFinal);

                if (authPark == null)//authenticatedParking != null)
                {
                    if (isBefore < 0)
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
                else
                {
                    return NotFound();

                }

            }
            return NotFound();

        }

        [HttpGet]
        public IActionResult MyBooking()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            ViewBag.userid = claims.FirstOrDefault().ToString();

            ViewBag.spots = _dbContext.ParkingLists.Select(column => column.Name).ToList();
            var nullableIntList = _dbContext.ParkingLists.Select(column => column.Id).ToList();
            ViewBag.spotsNum = nullableIntList.Select(id => id?.ToString() ?? "").ToList();
            ViewBag.spotsAddress = _dbContext.ParkingLists.Select(column => column.Address).ToList();
            return View(_dbContext.Bookings);
        }


    }
}
