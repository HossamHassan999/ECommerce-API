using WebApplication_API.Data;
using WebApplication_API.DTOs;

namespace WebApplication_API.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<IEnumerable<ProductDTO>> GetPaginationAsync(int gaemumber, int pagesize);
        Task<IEnumerable<ProductDTO>> SearchAsync(string keyword);

        Task<ProductDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ProductDTO>> GetByCategoryIdAsync(int categoryId);
        Task<ProductDTO> CreateAsync(ProductDTOPost productDto);
        Task<bool> UpdateAsync(int id, ProductDTOPost productDto);
        Task<bool> DeleteAsync(int id);
    }
}
