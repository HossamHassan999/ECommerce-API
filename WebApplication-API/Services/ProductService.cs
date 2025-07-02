using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApplication_API.Data;
using WebApplication_API.DTOs;
using WebApplication_API.Interfaces;

namespace WebApplication_API.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _context.Products.Include(p=>p.Category).ToListAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<IEnumerable<ProductDTO>> GetPaginationAsync(int gaemumber , int pagesize)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Skip((gaemumber - 1 ) * pagesize)
                .Take(pagesize)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }
        public async Task<IEnumerable<ProductDTO>> SearchAsync(string keyword)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Name.Contains(keyword) || p.Description.Contains(keyword))
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO?> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetByCategoryIdAsync(int categoryId)
        {
            var products = await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();

            if (!products.Any())
            {
                
                return Enumerable.Empty<ProductDTO>();
            }

            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> CreateAsync(ProductDTOPost productDto)
        {
           
            string fileName = $"{Guid.NewGuid()}_{productDto.Image.FileName}";
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await productDto.Image.CopyToAsync(stream);
            }

         
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                CategoryId = productDto.CategoryId,
                ImageUrl = $"/images/{fileName}" 
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDTO>(product);
        }


        public async Task<bool> UpdateAsync(int id, ProductDTOPost productDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Description = productDto.Description;
            product.CategoryId = productDto.CategoryId;

            if (productDto.Image != null)
            {
     
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.ImageUrl.TrimStart('/'));
                    if (File.Exists(oldPath))
                        File.Delete(oldPath);
                }

       
                string fileName = $"{Guid.NewGuid()}_{productDto.Image.FileName}";
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await productDto.Image.CopyToAsync(stream);
                }

                product.ImageUrl = $"/images/{fileName}";
            }

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
