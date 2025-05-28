using E_commerce_Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerApi : ControllerBase
    {
        private readonly myContext _context;

        public CustomerApi(myContext context)
        {
            _context = context;
        }

        // Get all customers
        [HttpGet("customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _context.tbl_customer.ToListAsync();
            return Ok(customers);
        }

        // Get customer by ID
        [HttpGet("customer/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _context.tbl_customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }
            return Ok(customer);
        }

        // Create a new customer
        [HttpPost("customer")]
        public async Task<IActionResult> CreateCustomer([FromBody] customer newCustomer)
        {
            if (newCustomer == null)
            {
                return BadRequest("Customer data is required.");
            }

            _context.tbl_customer.Add(newCustomer); // Corrected here
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomerById), new { id = newCustomer.customer_Id }, newCustomer);
        }

        // Update an existing customer
        [HttpPut("customer/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] customer updatedCustomer)
        {
            var customer = await _context.tbl_customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            customer.customer_name = updatedCustomer.customer_name;
            customer.customer_phone = updatedCustomer.customer_phone;
            customer.customer_eamil = updatedCustomer.customer_eamil;
            customer.customer_password = updatedCustomer.customer_password;
            customer.customer_image = updatedCustomer.customer_image;
            customer.customer_country = updatedCustomer.customer_country;
            customer.customer_city = updatedCustomer.customer_city;
            customer.customer_gender = updatedCustomer.customer_gender;
            customer.customer_address = updatedCustomer.customer_address;

            _context.tbl_customer.Update(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        // Delete a customer
        [HttpDelete("customer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.tbl_customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            _context.tbl_customer.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
