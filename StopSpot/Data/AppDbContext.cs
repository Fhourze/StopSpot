using Microsoft.EntityFrameworkCore;
using StopSpot.Models;

namespace StopSpot.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet <AccountModel> Accounts { get; set; }

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

            
        }
    }
}
