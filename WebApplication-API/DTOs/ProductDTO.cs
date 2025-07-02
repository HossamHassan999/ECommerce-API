namespace WebApplication_API.DTOs
{
public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }

    public int CategoryId { get; set; }

    public string CategoryName { get; set; } // from Category.Name
}

    public class ProductDTOPost
    {
  
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public IFormFile Image { get; set; } 
    }


}
