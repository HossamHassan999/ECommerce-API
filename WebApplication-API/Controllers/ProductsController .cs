using Microsoft.AspNetCore.Mvc;
using WebApplication_API.DTOs;
using WebApplication_API.Interfaces;

namespace WebApplication_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet("ByGategoryId")]
        public async Task<IActionResult> ByGategoryId(int categoryid)
        {
            var products = await _productService.GetByCategoryIdAsync(categoryid);
            if (products == null) return NotFound(new { massege = "No products found in this category." });
            return Ok(products);
        }

        [HttpGet("Paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1 , [FromQuery] int pagesize = 5 )
        {
            var products = await _productService.GetPaginationAsync(page, pagesize);

            if (products == null) return NotFound(new { massege = "No products found in this category." });
            return Ok(products);
        }


        [HttpGet("search")]
        public async Task<IActionResult> Search(string keyword)
        {
            var results = await _productService.SearchAsync(keyword);

            if (!results.Any())
                return NotFound(new { message = "No products matched your search." });

            return Ok(results);
        }


        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductDTOPost productDto)
        {
            var createdProduct = await _productService.CreateAsync(productDto);
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductDTOPost productDto)
        {
            var updated = await _productService.UpdateAsync(id, productDto);
            if (!updated) return NotFound();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
