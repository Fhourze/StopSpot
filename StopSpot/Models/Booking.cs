using System.ComponentModel.DataAnnotations;

namespace StopSpot.Models
{

    public class Booking
    {
        [Key]
        [Required]
        public int BookingId { get; set; }

        [Required]
        [Display(Name = "Parking Spot")]
        public string ParkingSpot { get; set; }


        [Required]
        [Display(Name = "Parking From")]
        public DateTime ParkingFrom { get; set; }

        [Required]
        [Display(Name = "Parking Until")]
        public DateTime ParkingUntil { get; set; }

        [Required]
        public int Total { get; set; }


    }
}