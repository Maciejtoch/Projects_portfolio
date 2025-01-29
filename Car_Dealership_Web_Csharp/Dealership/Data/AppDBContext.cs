using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Dealership.Models;  
using Microsoft.AspNetCore.Identity;

namespace Dealership.Data
{
    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        
        public DbSet<Car> Cars { get; set; }  
        public DbSet<Customer> Customers { get; set; }  
        public DbSet<Order> Orders { get; set; }  
        public DbSet<UserActivity> UserActivities { get; set; }  
        public DbSet<Offer> Offers { get; set; }

    }
}
