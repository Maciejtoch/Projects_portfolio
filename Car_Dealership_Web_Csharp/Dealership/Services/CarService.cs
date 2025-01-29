using Dealership.Data;
using Dealership.Models;
using Microsoft.EntityFrameworkCore;

namespace Dealership.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;

        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task AddCarAsync(Car car)
        {
            try
            {
                Console.WriteLine("Proba dodania auta ");
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                Console.WriteLine("Car added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding car to database: " + ex.Message);
            }
        }

        public async Task UpdateCarAsync(Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }
    }
}
