using System.ComponentModel.DataAnnotations;

namespace StopSpot.Models
{
    public class AccountModel
    {
        [Key]
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string AccountType { get; set; }
    }
}
