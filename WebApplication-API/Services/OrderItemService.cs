using WebApplication_API.Data;
using WebApplication_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebApplication_API.DTOs;
namespace WebApplication_API.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public OrderItemService(ApplicationDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderItemDTO>> GetAllAsync()
        {
            var orderitem =  await _context.OrderItems

                .Include(oi => oi.Product)
                .Include(oi => oi.Order)
                .ToListAsync();
            return _mapper.Map<IEnumerable<OrderItemDTO>>(orderitem);
        }

        public async Task<OrderItemDTO?> GetByIdAsync(int id)
        {

            var orderitemid = await _context.OrderItems
                .Include(oi => oi.Product)
                .Include(oi => oi.Order)
                .FirstOrDefaultAsync(oi => oi.Id == id);

            return _mapper.Map<OrderItemDTO>(orderitemid);

        }


        public async Task<OrderItemDTO> CreateAsync(OrderItemDTO orderItem)
        {
            var orderitem = _mapper.Map<OrderItem>(orderItem);
            _context.OrderItems.Add(orderitem);
            await _context.SaveChangesAsync();
            return orderItem;
        }


        public async Task<bool> UpdateAsync(OrderItemDTO orderItem)
        {
            var existing = await _context.OrderItems.FindAsync(orderItem.Id);
            if (existing == null) return false;

            existing.Quantity = orderItem.Quantity;
            existing.Price = orderItem.Price;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item == null) return false;

            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
