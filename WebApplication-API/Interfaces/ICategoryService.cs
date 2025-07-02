using WebApplication_API.Data;
using WebApplication_API.DTOs;

namespace WebApplication_API.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO?> GetByIdAsync(int id);
        Task<CategoryDTO> CreateAsync(CategoryDTO category);
        Task<bool> UpdateAsync(CategoryDTO category);
        Task<bool> DeleteAsync(int id);
    }
}
