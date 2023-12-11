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

        public AccountModel GetAccountByEmailAndPassword(string email, string password)
        {

            return Accounts.FirstOrDefault(a => a.Email == email && a.Password == password);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }
    }

}
