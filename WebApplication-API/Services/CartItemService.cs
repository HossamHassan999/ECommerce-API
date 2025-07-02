using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WebApplication_API.Data;
using WebApplication_API.DTOs;
using WebApplication_API.Interfaces;

namespace WebApplication_API.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CartItemService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CartItemDTO>> GetAllAsync()
        {
            var cartitems = await _context.CartItems.Include(c => c.Product).Include(c => c.User).ToListAsync();

            return _mapper.Map<IEnumerable<CartItemDTO>>(cartitems);

        }   

        public async Task<CartItemDTO?> GetByIdAsync(int id)
        {
            var cartitem =  await _context.CartItems
                .Include(c => c.Product)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);

            return cartitem == null ? null : _mapper.Map<CartItemDTO>(cartitem);
        }    

        public async Task<CartItemDTOPost> CreateAsync(CartItemDTOPost cartItem)
        {
           var createcartItem = _mapper.Map<CartItem>(cartItem);

            _context.CartItems.Add(createcartItem);
            await _context.SaveChangesAsync();
            return _mapper.Map<CartItemDTOPost>(cartItem);
        }

        public async Task<bool> UpdateAsync(CartItemDTO cartItem)
        {
            var existing = await _context.CartItems.FindAsync(cartItem.Id);
            if (existing == null) return false;

            existing.Quantity = cartItem.Quantity;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item == null) return false;

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}