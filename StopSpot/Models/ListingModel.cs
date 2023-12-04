using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace StopSpot.Models
{
    public class ListingModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string Picture { get; set; }
        public string Vehicles { get; set; }
        public int PricePerHour { get; set; }
        public string OwnerNumber { get; set; }
        public string Availability { get; set; }

    }
}