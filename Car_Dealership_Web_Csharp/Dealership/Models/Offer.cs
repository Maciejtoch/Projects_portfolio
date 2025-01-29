using System.ComponentModel.DataAnnotations;

namespace Dealership.Models
{
    public class Offer
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public decimal Price { get; set; }

        public string Message { get; set; }

        public int CarId { get; set; }

        [Required]
        public DateTime DateSubmitted { get; set; }

        public Car Car { get; set; }
        public ApplicationUser User { get; set; }
    }

}
