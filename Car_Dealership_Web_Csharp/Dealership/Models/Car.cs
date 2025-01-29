using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Price { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
    }
}