using Microsoft.EntityFrameworkCore;
using StopSpot.Models;

namespace StopSpot.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ListingModel> ParkingLists { get; set; }   


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public AccountModel GetAccountByEmail(string email)
        {
            return Accounts.FirstOrDefault(a => a.Email == email);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }
    }

}
