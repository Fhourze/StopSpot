using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StopSpot.Data;
using StopSpot.Models;



namespace StopSpot.Controllers
{
    public class ListingController : Controller
    {
        private readonly ListingDbContext _listdbContext;
        private readonly IWebHostEnvironment _environment;
        public ListingController(ListingDbContext ListdbContext, IWebHostEnvironment environment)
        {
            _listdbContext = ListdbContext;
            _environment = environment;
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
            string folder = "Listing/Images/";
            string serverFolder = Path.Combine(_environment.WebRootPath, folder);
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + newListing.UploadedPhoto.FileName;
            string filePath = Path.Combine(serverFolder, uniqueFileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            {
                newListing.UploadedPhoto.CopyTo(fileStream);
            }
            newListing.Picture = folder + uniqueFileName;


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
                string folder = "Listing/Images/";
                string serverFolder = Path.Combine(_environment.WebRootPath, folder);
                string uniqueFileName = Guid.NewGuid().ToString() + "___" + listingModelChange.UploadedPhoto.FileName;
                string filePath = Path.Combine(serverFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                {
                    listingModelChange.UploadedPhoto.CopyTo(fileStream);
                }
                listingModelChange.Picture = folder + uniqueFileName;

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



       /* public IActionResult Upload(IFormFile files)
        {
            if (files != null)
            {
                if (files.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                    var objfiles = new ListingModel()
                    {

                        //PicId = 0,
                        //Name = newFileName,
                        //FileType = fileExtension,
                        //CreatedOn = DateTime.Now
                    };

                    using (var target = new MemoryStream())
                    {
                        files.CopyTo(target);
                        objfiles.Picture = target.ToArray();
                    }

                    _listdbContext.ParkingLists.Add(objfiles);
                    _listdbContext.SaveChanges();

                }
            }
            return View();
        }*/

    }
}
