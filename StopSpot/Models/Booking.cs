﻿using System.ComponentModel.DataAnnotations;

namespace StopSpot.Models
{
    public enum Spots
    {
        McDoParking, AssistantProfessor, AssociateProfessor, Professor
    }
    public class Booking
    {
        [Key]
        [Required]
        public int BookingId { get; set; }

        [Required]
        [Display(Name = "Parking Spot")]
        public Spots ParkingSpot { get; set; }


        [Required]
        [Display(Name = "Parking From")]
        public DateTime ParkingFrom { get; set; }

        [Required]
        [Display(Name = "Parking Until")]
        public DateTime ParkingUntil { get; set; }

        public int Total { get; set; }
        

    }
}