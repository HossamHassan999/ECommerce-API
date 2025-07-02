using WebApplication_API.Data;
using WebApplication_API.DTOs;

namespace WebApplication_API.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetAllAsync();
        Task<OrderDTO?> GetByIdAsync(int id);
        Task<OrderDTO> CreateAsync(OrderDTOPost order);
        Task<bool> UpdateAsync(OrderDTO order);
        Task<bool> DeleteAsync(int id);
    }
}
