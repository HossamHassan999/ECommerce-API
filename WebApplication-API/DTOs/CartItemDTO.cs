namespace WebApplication_API.DTOs
{
    public class CartItemDTO
    {
        public int Id { get; set; }

        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }
    }

    public class CartItemDTOPost
    {
        
        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
