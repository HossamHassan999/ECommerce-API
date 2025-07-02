namespace WebApplication_API.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }

    }
    public class OrderDTOPost
    {
        public int UserId { get; set; }

    }
}
