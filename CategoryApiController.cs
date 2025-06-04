using E_commerce_Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly myContext _context;

        public CategoryApiController(myContext context)
        {
            _context = context;
        }

        // Get all categories
        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _context.tbl_category.Include(c => c.products).ToListAsync();
            return Ok(categories);
        }

        // Get category by ID
        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _context.tbl_category.Include(c => c.products)
                                                     .FirstOrDefaultAsync(c => c.category_Id == id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }
            return Ok(category);
        }

        // Create a new category
        [HttpPost("category")]
        public async Task<IActionResult> CreateCategory([FromBody] category newCategory)
        {
            if (newCategory == null)
            {
                return BadRequest("Category data is required.");
            }

            _context.tbl_category.Add(newCategory);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.category_Id }, newCategory);
        }

        // Update an existing category
        [HttpPut("category/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] category updatedCategory)
        {
            var category = await _context.tbl_category.FindAsync(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            category.category_Name = updatedCategory.category_Name;
            _context.tbl_category.Update(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }

        // Delete a category
        [HttpDelete("category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.tbl_category.FindAsync(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            _context.tbl_category.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
