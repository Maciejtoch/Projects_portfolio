using Dealership.Services;
using Dealership.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Dealership.Data;

namespace Dealership.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ICarService _carService;
        private readonly ILogger<AdminController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context, ICarService carService, ILogger<AdminController> logger, UserManager<ApplicationUser> userManager)
        {
            _carService = carService;
            _logger = logger;
            _userManager = userManager;
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userManager.Users.ToListAsync(); 

            if (users == null)
            {
                users = new List<ApplicationUser>();
            }

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Message"] = "Wybierz użytkownika do usunięcia.";
                return RedirectToAction("ManageUsers");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData["Message"] = "Użytkownik został usunięty.";
                }
                else
                {
                    TempData["Message"] = "Wystąpił problem podczas usuwania użytkownika.";
                }
            }
            else
            {
                TempData["Message"] = "Użytkownik nie istnieje.";
            }

            return RedirectToAction("ManageUsers");
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(Car car)
        {
            _logger.LogInformation("POST /Admin/AddCar triggered");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state.");
                return View(car);
            }

            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    _logger.LogInformation("Image file received.");

                    var imageFile = Request.Form.Files[0];
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                        if (!Directory.Exists(imageDirectory))
                        {
                            Directory.CreateDirectory(imageDirectory);
                        }

                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                        var filePath = Path.Combine(imageDirectory, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        car.ImageUrl = $"/images/{fileName}";
                        _logger.LogInformation($"Image saved locally at: {car.ImageUrl}");
                    }
                }

                await _carService.AddCarAsync(car);

                TempData["SuccessMessage"] = "Samochód został pomyślnie dodany!";
                return RedirectToAction("AddCar");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding car: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Wystąpił błąd podczas dodawania samochodu.");
            }

            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _carService.GetCarByIdAsync(id); 
            if (car != null)
            {
                await _carService.DeleteCarAsync(id); 
                return RedirectToAction("Index", "Cars");
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> AdminOffers()
        {
            var offers = await _context.Offers.Include(o => o.Car).ToListAsync();
            return View(offers);
        }

    }
}