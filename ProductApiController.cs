using E_commerce_Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly myContext _context;

        public ProductApiController(myContext context)
        {
            _context = context;
        }

        // Get all products
        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.tbl_product.Include(p => p.category).ToListAsync();
            return Ok(products);
        }

        // Get product by ID
        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _context.tbl_product.Include(p => p.category)
                                                 .FirstOrDefaultAsync(p => p.product_Id == id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            return Ok(product);
        }

        // Create a new product
        [HttpPost("product")]
        public async Task<IActionResult> CreateProduct([FromBody] product newProduct)
        {
            if (newProduct == null)
            {
                return BadRequest("Product data is required.");
            }

            _context.tbl_product.Add(newProduct);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.product_Id }, newProduct);
        }

        // Update an existing product
        [HttpPut("product/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] product updatedProduct)
        {
            var product = await _context.tbl_product.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            product.product_name = updatedProduct.product_name;
            product.product_description = updatedProduct.product_description;
            product.product_price = updatedProduct.product_price;
            product.product_image = updatedProduct.product_image;
            product.cat_id = updatedProduct.cat_id;

            _context.tbl_product.Update(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        // Delete a product
        [HttpDelete("product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.tbl_product.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            _context.tbl_product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
