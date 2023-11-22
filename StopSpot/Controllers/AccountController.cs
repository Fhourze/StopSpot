using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
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

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AccountModel account)
        {
            if (ModelState.IsValid)
            {
                // Validate the user's credentials
                AccountModel authenticatedAccount = _dbContext.GetAccountByEmailAndPassword(account.Email, account.Password);

                if (authenticatedAccount != null)
                {
                    // Redirect to dashboard or perform other actions upon successful login
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    // Display login failure message
                    ViewBag.ErrorMessage = "Invalid email or password";
                    return View(account);
                }
            }

            // If the model state is not valid, return to the login page
            return View(account);
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(AccountModel account)
        {
            if (ModelState.IsValid)
            {
                // Check if the email is already registered
                if (_dbContext.Accounts.Any(a => a.Email == account.Email))
                {
                    ModelState.AddModelError("Email", "Email is already registered.");
                    return View(account);
                }

                // Save the new account to the database
                _dbContext.Accounts.Add(account);
                _dbContext.SaveChanges();

                // Redirect to login page after successful registration
                return RedirectToAction("Login");
            }

            // If the model state is not valid, return to the registration page
            return View(account);
        }


        // GET: Account/Logout
        public IActionResult Logout()
        {
            // Sign out the user
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to the login page after logout
            return RedirectToAction("Login", "Account");
        }



    }
    }
