using Dealership.Services;
using Dealership.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dealership.Data;

namespace Dealership.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService _carService;
        private readonly ApplicationDbContext _context;
  
        public CarsController(ICarService carService, ApplicationDbContext context)
        {
            _carService = carService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAllCarsAsync();
            return View(cars);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                await _carService.AddCarAsync(car);
                return RedirectToAction(nameof(Index)); 
            }
            return View(car); 
        }

        public async Task<IActionResult> Edit(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound(); 
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest(); 
            }

            if (ModelState.IsValid)
            {
                await _carService.UpdateCarAsync(car);
                return RedirectToAction(nameof(Index)); 
            }

            return View(car); 
        }

        public async Task<IActionResult> Details(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound(); 
            }
            return View(car);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound(); 
            }
            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _carService.DeleteCarAsync(id);
            return RedirectToAction(nameof(Index)); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitOffer(string firstName, string lastName, string phoneNumber, decimal price, string message, int carId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var offer = new Offer
                {
                    UserId = User.Identity.Name,
                    CarId = carId,
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    Price = price,
                    Message = message,
                    DateSubmitted = DateTime.Now
                };

                _context.Offers.Add(offer);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Your offer has been submitted successfully.";
            }
            else
            {
                TempData["Message"] = "You must be logged in to make an offer.";
            }

            return RedirectToAction("CarDetails", new { id = carId });
        }

        public async Task<IActionResult> AdminOffers(int carId)
        {
            if (User.IsInRole("Admin"))
            {
                var offers = await _context.Offers
                    .Where(o => o.CarId == carId)
                    .ToListAsync();

                return View(offers);
            }

            TempData["Message"] = "You must be an admin to view offers.";
            return RedirectToAction("Index");
        }


    }
}
