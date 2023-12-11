using Microsoft.AspNetCore.Mvc;
using StopSpot.Data;
using StopSpot.Models;
using System.Linq;

namespace StopSpot.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AccountController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AccountModel loginInfo)
        {
            
                var user = _dbContext.Accounts.FirstOrDefault(u => u.Email == loginInfo.Email && u.Password == loginInfo.Password);

                if (user != null)
                {
                    // Successful login logic (e.g., setting up a session)
                    ViewBag.SuccessMessage = "Login successful!";

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid email or password.";
                    return View(loginInfo);
                }
            


            return View(loginInfo);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountModel registerInfo)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Accounts.Add(registerInfo);
                _dbContext.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(registerInfo);
        }

        public IActionResult Edit()
        {
            // Retrieve the last account from the database
            var lastAccount = _dbContext.Accounts.OrderByDescending(a => a.AccountId).FirstOrDefault();

            if (lastAccount == null)
            {
                ViewBag.ErrorMessage = "No accounts found.";
                return View();
            }

            return View(lastAccount);
        }

        [HttpPost]
        public IActionResult Edit(AccountModel editedAccount)
        {
            if (ModelState.IsValid)
            {
                // Update the account in the database
                _dbContext.Accounts.Update(editedAccount);
                _dbContext.SaveChanges();
                ViewBag.SuccessMessage = "Account updated successfully!";
                return RedirectToAction("Index", "Home");
            }

            return View(editedAccount);
        }






        public IActionResult Logout()
        {
            // Perform logout actions (if needed)
            return RedirectToAction("Login");
        }


        public IActionResult DeleteLastAccount()
        {
            var lastAccount = _dbContext.Accounts.OrderByDescending(a => a.AccountId).FirstOrDefault();

            if (lastAccount != null)
            {
                _dbContext.Accounts.Remove(lastAccount);
                _dbContext.SaveChanges();
                ViewBag.SuccessMessage = "Last account deleted successfully!";
            }
            else
            {
                ViewBag.ErrorMessage = "No accounts found to delete.";
            }

            return RedirectToAction("Login", "Account");
        }
    }
}
