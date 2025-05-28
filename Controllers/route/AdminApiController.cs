using E_commerce_Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly myContext _context;

        public AdminApiController(myContext context)
        {
            _context = context;
        }

        // Test API
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Admin API is working!");
        }

        // Get all admins
        [HttpGet("admins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await _context.tbl_admin.ToListAsync();
            return Ok(admins);
        }

        // Get admin by ID
        [HttpGet("admin/{id}")]
        public async Task<IActionResult> GetAdminById(int id)
        {
            var admin = await _context.tbl_admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound("Admin not found.");
            }
            return Ok(admin);
        }

        // Create a new admin
        [HttpPost("admin")]
        public async Task<IActionResult> CreateAdmin([FromBody] Admin newAdmin)
        {
            if (newAdmin == null)
            {
                return BadRequest("Admin data is required.");
            }

            _context.tbl_admin.Add(newAdmin);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAdminById), new { id = newAdmin.Id }, newAdmin);
        }

        // Update an existing admin
        [HttpPut("admin/{id}")]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] Admin updatedAdmin)
        {
            var admin = await _context.tbl_admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound("Admin not found.");
            }

            admin.admin_name = updatedAdmin.admin_name;
            admin.admin_email = updatedAdmin.admin_email;
            admin.admin_password = updatedAdmin.admin_password;
            admin.admin_image = updatedAdmin.admin_image;

            _context.tbl_admin.Update(admin);
            await _context.SaveChangesAsync();

            return Ok(admin);
        }

        // Delete an admin
        [HttpDelete("admin/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _context.tbl_admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound("Admin not found.");
            }

            _context.tbl_admin.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
