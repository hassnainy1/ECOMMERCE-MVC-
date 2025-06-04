using E_commerce_Website.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace E_commerce_Website.Controllers
{
    public class existingCustomer : Controller
    {
        private readonly myContext _context;
        private readonly IWebHostEnvironment _env;

        public existingCustomer(myContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Edit Customer View
        public IActionResult customerUpdate(int id)
        {
            var customer = _context.tbl_customer.FirstOrDefault(c => c.customer_Id == id);
            return View(customer);
        }

        // POST: Update Customer
        [HttpPost]
        public IActionResult customerUpdate(customer updatedCustomer, IFormFile customer_image)
        {
            var existingCustomer = _context.tbl_customer.Find(updatedCustomer.customer_Id);

            if (existingCustomer == null)
                return NotFound();

            // Keep password if not updating
            updatedCustomer.customer_password = existingCustomer.customer_password;

            // Handle image upload
            if (customer_image != null && customer_image.Length > 0)
            {
                string folderPath = Path.Combine(_env.WebRootPath, "customer_image");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, customer_image.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    customer_image.CopyTo(stream);
                }

                updatedCustomer.customer_image = customer_image.FileName;
            }
            else
            {
                updatedCustomer.customer_image = existingCustomer.customer_image;
            }

            _context.Entry(existingCustomer).CurrentValues.SetValues(updatedCustomer);
            _context.SaveChanges();

            return RedirectToAction("fetchcustomer", "Admin");
        }
    }
}
