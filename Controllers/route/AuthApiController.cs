using E_commerce_Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_Website.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly myContext _context;

        public AuthApiController(myContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] string adminEmail, [FromForm] string adminPassword)
        {
            var row = _context.tbl_admin.FirstOrDefault(a => a.admin_email == adminEmail);
            if (row != null && row.admin_password == adminPassword)
            {
                return Ok(new { success = true, message = "Login successful", adminId = row.Id });
            }

            return Unauthorized(new { success = false, message = "Incorrect username or password" });
        }
    }
}
