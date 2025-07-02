using Microsoft.AspNetCore.Mvc;
using WebApplication_API.Interfaces;
using WebApplication_API.Data;
using WebApplication_API.DTOs;

namespace WebApplication_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemsController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _orderItemService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _orderItemService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderItemDTO orderItem)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _orderItemService.CreateAsync(orderItem);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderItemDTO orderItem)
        {
            if (id != orderItem.Id) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _orderItemService.UpdateAsync(orderItem);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _orderItemService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
