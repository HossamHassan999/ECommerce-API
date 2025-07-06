using WebApplication_API.Data;
using WebApplication_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebApplication_API.DTOs;
using System.Text;
namespace WebApplication_API.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IInvoiceBuilderService _invoiceBuilder;
        public OrderService(ApplicationDbContext context , IMapper mapper  , IEmailService emailService , IInvoiceBuilderService invoiceBuilder)
        {
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
            _invoiceBuilder = invoiceBuilder;
        }




        public async Task<IEnumerable<OrderDTO>> GetAllAsync()
        {
            var order =  await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDTO>>(order);
        }




        public async Task<OrderDTO?> GetByIdAsync(int id)
        {
            var orderbyid = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                 .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
            return _mapper.Map<OrderDTO>(orderbyid);
        }






        public async Task<OrderDTO?> CreateAsync(OrderDTOPost order)
        {
            var cartItems = await _context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.UserId == order.UserId)
                .ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                return null;
            }

            var newOrder = _mapper.Map<Order>(order);

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            var orderItems = cartItems.Select(item => new OrderItem
            {
                OrderId = newOrder.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Product.Price
            }).ToList();

            _context.OrderItems.AddRange(orderItems);
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            // Get user info for email
            var user = await _context.Users.FindAsync(order.UserId);

            if (user != null)
            {
                string html = _invoiceBuilder.BuildInvoiceHtml(newOrder, orderItems, user);

                await _emailService.SendInvoiceEmailAsync(
                    user.Email,
                    $"Order Confirmation - Order #{newOrder.Id}",
                    html
                );
            }

            var result = _mapper.Map<OrderDTO>(newOrder);
            return result;
        }


        public async Task<bool> UpdateAsync(OrderDTO order)
        {
            var existing = await _context.Orders.FindAsync(order.Id);

            if (existing == null) return false;

            existing.Status = order.Status;

            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> DeleteAsync(int id)
        {

            var order = await _context.Orders.FindAsync(id);

            if (order == null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
