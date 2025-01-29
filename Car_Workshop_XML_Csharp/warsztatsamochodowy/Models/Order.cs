namespace warsztatsamochodowy.Models
{

    public class Order
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; } 
        public string Description { get; set; }

        
        public int CarId { get; set; }
        public Car Car { get; set; } 

        public int ClientId { get; set; }
        public Client Client { get; set; } 
    }
}