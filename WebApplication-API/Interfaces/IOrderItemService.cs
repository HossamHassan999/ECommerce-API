using WebApplication_API.Data;
using WebApplication_API.DTOs;

namespace WebApplication_API.Interfaces
{

    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDTO>> GetAllAsync();
        Task<OrderItemDTO?> GetByIdAsync(int id);
        Task<OrderItemDTO> CreateAsync(OrderItemDTO orderItem);
        Task<bool> UpdateAsync(OrderItemDTO orderItem);
        Task<bool> DeleteAsync(int id);
    }
}
