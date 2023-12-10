using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StopSpot.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace StopSpot.Data
{
    [Table("ParkingLists")]
    public class ListingDbContext : DbContext
    {

        public DbSet<ListingModel> ParkingLists { get; set; }

        public ListingDbContext(DbContextOptions<ListingDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ListingModel>().HasData(
            new ListingModel()
            {
                Id = 1,
                Name = "Test",
                OwnerName = "Test",
                Picture = "Test",
                Vehicles = "Test",
                PricePerHour = "Test",
                OwnerNumber = "Test",
                Availability = "true"
            }
            );

        }
    }
}