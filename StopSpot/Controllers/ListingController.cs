using Microsoft.AspNetCore.Mvc;
using StopSpot.Data;
using StopSpot.Models;


namespace StopSpot.Controllers
{
    public class ListingController : Controller
    {
        private readonly AppDbContext _listdbContext;
        public ListingController(AppDbContext ListdbContext)
        {
            _listdbContext = ListdbContext;
        }

        public IActionResult Index()
        {

            return View(_listdbContext.ParkingLists);
        }

        [HttpGet]
        public IActionResult AddListing()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddListing(ListingModel newListing)
        {
            _listdbContext.ParkingLists.Add(newListing);
            _listdbContext.SaveChanges();
            return RedirectToAction("Index", _listdbContext.ParkingLists);
        }

        [HttpGet]
        public IActionResult EditListingSpot(int id)
        {
            ListingModel? listingModel = _listdbContext.ParkingLists.FirstOrDefault(st => st.Id == id);

            if (listingModel != null)
                return View(listingModel);

            return NotFound();
        }
        [HttpPost]
        public IActionResult EditListingSpot(ListingModel listingModelChange, int id)
        {
            ListingModel? listingModel = _listdbContext.ParkingLists.FirstOrDefault(st => st.Id == listingModelChange.Id);
            if (listingModel != null)
            {
                listingModel.Name = listingModelChange.Name;
                listingModel.OwnerName = listingModelChange.OwnerName;
                listingModel.Picture = listingModelChange.Picture;
                listingModel.Vehicles = listingModelChange.Vehicles;
                listingModel.PricePerHour = listingModelChange.PricePerHour;
                listingModel.OwnerNumber = listingModelChange.OwnerNumber;
                listingModel.Availability = listingModelChange.Availability;
                _listdbContext.SaveChanges();

            }
            return View("Index", _listdbContext.ParkingLists);
        }

        [HttpGet]
        public IActionResult DeleteListingSpot(int id)
        {
            ListingModel? listingModel = _listdbContext.ParkingLists.FirstOrDefault(st => st.Id == id);

            if (listingModel != null)
                return View(listingModel);

            return NotFound();
        }

        [HttpPost]

        public IActionResult DeleteListingSpot(ListingModel deleteListing, int id)
        {
            ListingModel? listing = _listdbContext.ParkingLists.FirstOrDefault(st => st.Id == id);

            if (listing != null)
            {
                _listdbContext.ParkingLists.Remove(listing);
                _listdbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

    }
}