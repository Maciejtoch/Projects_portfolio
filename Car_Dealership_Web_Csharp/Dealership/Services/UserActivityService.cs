using Dealership.Data;
using Dealership.Models;
using Microsoft.EntityFrameworkCore;

namespace Dealership.Services
{
    public class UserActivityService : IUserActivityService
    {
        private readonly ApplicationDbContext _context;

        public UserActivityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserActivity>> GetUserActivitiesAsync()
        {
            return await _context.UserActivities.ToListAsync();
        }

        public async Task LogActivityAsync(UserActivity activity)
        {
            _context.UserActivities.Add(activity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserActivity>> GetAllActivitiesAsync()
        {
            return await _context.UserActivities.ToListAsync(); 
        }
    }
}
