using WebApplication_API.Data;
using WebApplication_API.DTOs;

namespace WebApplication_API.Interfaces
{
    public interface ICartItemService
    {
        Task<IEnumerable<CartItemDTO>> GetAllAsync();
        Task<CartItemDTO?> GetByIdAsync(int id);
        Task<CartItemDTOPost> CreateAsync(CartItemDTOPost cartItem);
        Task<bool> UpdateAsync(CartItemDTO cartItem);
        Task<bool> DeleteAsync(int id);
    }
}
