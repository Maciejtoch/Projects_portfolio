using Microsoft.AspNetCore.Identity;
using Dealership.Models;


namespace Dealership.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

    }
}
