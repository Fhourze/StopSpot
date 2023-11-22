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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AccountModel loginModel)
        {
  
                // Validate the user's credentials
                AccountModel authenticatedAccount = _dbContext.GetAccountByEmailAndPassword(loginModel.Email, loginModel.Password);

                if (authenticatedAccount != null)
                {
                // Perform any necessary actions upon successful login
                // For instance, setting authentication cookies or user sessions

                // Store user's first name in TempData to display in layout
                TempData["FirstName"] = authenticatedAccount.FirstName;


                // Redirect to a desired page upon successful login
                return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Display login failure message
                    ModelState.AddModelError(string.Empty, "Invalid email or password");
                    return View(loginModel);
                }
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
