using Dealership.Models;
using Dealership.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Dealership.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarService _carService;

        public HomeController(ILogger<HomeController> logger, ICarService carService)
        {
            _logger = logger;
            _carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            
            var featuredCarIds = new List<int> { 1, 2, 3 };

            var featuredCars = new List<Car>();
            foreach (var id in featuredCarIds)
            {
                var car = await _carService.GetCarByIdAsync(id);
                if (car != null)
                {
                    featuredCars.Add(car);
                    _logger.LogInformation($"Loaded car with ID {car.Id} - {car.Make} {car.Model}");
                }
                else
                {
                    _logger.LogWarning($"Car with ID {id} not found");
                }
            }

            return View(featuredCars);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
