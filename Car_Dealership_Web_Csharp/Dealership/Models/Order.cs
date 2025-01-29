using Microsoft.EntityFrameworkCore;


namespace Dealership.Models
{
    public enum OrderStatus
    {
        Pending,
        Completed,
        Cancelled
    }

    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }  
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }  
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
    }


}
