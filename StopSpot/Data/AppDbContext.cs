using Microsoft.EntityFrameworkCore;
using StopSpot.Models;

namespace StopSpot.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet <AccountModel> Accounts { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ListingModel> ParkingLists { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public AccountModel GetAccountByEmailAndPassword(string email, string password)
        {
            return Accounts.FirstOrDefault(account =>
                account.Email == email && account.Password == password);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AccountModel>().HasData(

                                new AccountModel()
                                {
                                    AccountId = 1,
                                    FirstName = "Test",
                                    LastName = "Test",
                                    Email = "Test",
                                    PhoneNumber = "Test",
                                    Password = "Test",
                                    AccountType = "Test",
                                }

                );

            modelBuilder.Entity<Booking>().HasData(

                                new Booking()
                                {
                                    BookingId = 1,
                                    ParkingSpot = "McDoParking",
                                    ParkingFrom = DateTime.Parse("2020-01-21"),
                                    ParkingUntil = DateTime.Parse("2020-01-22"),
                                    Total = 192
                                }

                );
            modelBuilder.Entity<ListingModel>().HasData(

                    new ListingModel()
                                {
                                    Id = 1,
                                    Name = "Test Motel",
                                    OwnerName = "TestOwner",
                                    Picture = "Test",
                                    Vehicles = "Test",
                                    PricePerHour = 9,
                                    OwnerNumber = "Test",
                                    Availability = "true"
                                }
                );

        }


    }
}
