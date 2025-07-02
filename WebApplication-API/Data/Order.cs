namespace WebApplication_API.Data
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";

        public ICollection<OrderItem> OrderItems { get; set; }
    }

}
