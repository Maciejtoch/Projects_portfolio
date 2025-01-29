using Dealership.Data;
using Dealership.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ApplicationDbContext _context;

    public AccountController(ILogger<AccountController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context; 
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            Console.WriteLine("Rejestracja zakończona sukcesem");
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Console.WriteLine("Rejestracja nie powiodła się");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return View(model);
    }
    
    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public IActionResult Register()
    {
        return View();
    }

    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Nieprawidłowa próba logowania.");
        }
        return View(model);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Logout()
    {
        var userId = _userManager.GetUserId(User);
        if (userId != null)
        {
            _context.UserActivities.Add(new UserActivity { UserId = userId, Action = "Wylogowanie", Timestamp = DateTime.Now });
            await _context.SaveChangesAsync();
        }

        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
