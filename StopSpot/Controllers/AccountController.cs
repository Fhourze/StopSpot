using Microsoft.AspNetCore.Mvc;
using StopSpot.Models;
using StopSpot.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

public class AccountController : Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Register
    public IActionResult Register()
    {
        return View();
    }

    // POST: Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(AccountModel account)
    {
        if (ModelState.IsValid)
        {
            // Check if the email is already registered
            if (_context.Accounts.Any(a => a.Email == account.Email))
            {
                ModelState.AddModelError("Email", "Email is already in use");
                return View(account);
            }

            _context.Accounts.Add(account);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        return View(account);
    }

    // GET: Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(Login login)
    {
        if (ModelState.IsValid)
        {
            var user = _context.GetAccountByEmail(login.Email);

            if (user != null && user.Password == login.Password)
            {
                var claims = new List<Claim>
                    {
                        new Claim("UserId", user.AccountId.ToString())
                        // Add more claims if needed for authorization or other purposes
                    };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                    {
                        // Customize authentication properties if needed
                        IsPersistent = true // Set to true for persistent cookies
                    };

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties).Wait(); // Synchronously sign in the user

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid credentials";
            }
        }

        return View(login);
    }


    // Logout
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait(); // Synchronously sign out the user
        HttpContext.Session.Clear(); // Clear the session data if needed

        return RedirectToAction("Login", "Account"); // Redirect to home or any other page after logout
    }



    public IActionResult Edit()
    {
        if (User.Identity.IsAuthenticated)
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                var user = _context.Accounts.FirstOrDefault(a => a.AccountId == userId);
                if (user != null)
                {
                    return View(user);
                }
            }
        }
        return RedirectToAction("Login", "Account");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AccountModel model)
    {
        if (ModelState.IsValid)
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                var existingUser = await _context.Accounts.FindAsync(userId);
                if (existingUser != null)
                {
                    existingUser.FirstName = model.FirstName;
                    existingUser.LastName = model.LastName;
                    existingUser.Email = model.Email;
                    existingUser.PhoneNumber = model.PhoneNumber;

                    // Update the password only if the user has provided a new one
                    if (!string.IsNullOrWhiteSpace(model.Password))
                    {
                        existingUser.Password = model.Password;
                        // Here you should hash and store the updated password, similar to how it's done during registration
                        
                    }

                    existingUser.AccountType = model.AccountType;

                    _context.Entry(existingUser).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
            }
        }
        return View(model); // If ModelState is not valid or user doesn't exist, return to the edit view with the model
    }



    // Delete action
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _context.Accounts.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Accounts.Remove(user);
        await _context.SaveChangesAsync();

        // Log the user out after deletion (optional)
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Login", "Account");
    }






}
