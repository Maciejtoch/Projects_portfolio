using Dealership.Models;
using Microsoft.EntityFrameworkCore;

namespace Dealership.Services
{
    public interface IUserActivityService
    {
        Task<IEnumerable<UserActivity>> GetUserActivitiesAsync();
        Task LogActivityAsync(UserActivity activity);
        Task<IEnumerable<UserActivity>> GetAllActivitiesAsync();
    }
}
