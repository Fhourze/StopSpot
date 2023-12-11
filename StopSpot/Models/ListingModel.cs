using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StopSpot.Models
{
    public class ListingModel
    {
        [Key]
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? OwnerName { get; set; }
        public string? Vehicles { get; set; }
        public string? PricePerHour { get; set; }
        public string? OwnerNumber { get; set; }
        public string? Availability { get; set; }

        [NotMapped]
        public IFormFile? UploadedPhoto { get; set; }

        [Display(Name = "ParkingPic")]
        public string? Picture {  get; set; }

    }
}
