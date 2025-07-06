using Microsoft.AspNetCore.Mvc;
using WebApplication_API.Interfaces;
using WebApplication_API.Data;
using WebApplication_API.DTOs;

namespace WebApplication_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDTOPost order)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _orderService.CreateAsync(order);

            return Ok(created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderDTO order)
        {
            if (id != order.Id) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _orderService.UpdateAsync(order);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _orderService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
