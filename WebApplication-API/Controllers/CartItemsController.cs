using Microsoft.AspNetCore.Mvc;
using WebApplication_API.Interfaces;
using WebApplication_API.Data;
using WebApplication_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApplication_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemsController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cartItems = await _cartItemService.GetAllAsync();
            return Ok(cartItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cartItem = await _cartItemService.GetByIdAsync(id);
            if (cartItem == null) return NotFound();
            return Ok(cartItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CartItemDTOPost cartItem)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _cartItemService.CreateAsync(cartItem);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CartItemDTO cartItem)
        {
            if (id != cartItem.Id) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _cartItemService.UpdateAsync(cartItem);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _cartItemService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
