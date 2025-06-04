using E_commerce_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Website.Controllers
{

    public class AdminController : Controller
    {
        private myContext _context;
        private IWebHostEnvironment _env;
        public AdminController(myContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()

        {
            string admin_session = HttpContext.Session.GetString("admin_session");
            if (admin_session != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("login");

            }
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(String adminEmail, string adminPassword)
        {
            var row = _context.tbl_admin.FirstOrDefault(a => a.admin_email == adminEmail);
            if (row != null && row.admin_password == adminPassword)
            {
                HttpContext.Session.SetString("admin_session", row.Id.ToString());
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Incorrect username or password";
                return View();
            }

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("admin_session");
            return RedirectToAction("login");
        }
        public IActionResult Profile()
        {
            // Get admin ID from session
            string adminId = HttpContext.Session.GetString("admin_session");

            // Redirect to login if session doesn't exist
            if (adminId == null)
            {
                return RedirectToAction("Login");
            }

            // Fetch admin details from DB
            var admin = _context.tbl_admin.FirstOrDefault(a => a.Id == Convert.ToInt32(adminId));

            if (admin == null)
            {
                // If no admin is found, redirect to login or error page
                return RedirectToAction("Login");
            }

            // Return the Profile view with the admin data
            return View(admin);
        }

        [HttpPost]
        public IActionResult Profile(Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.tbl_admin.Update(admin);
                _context.SaveChanges();
            }
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfileImage(IFormFile admin_image, int Id)
        {
            if (admin_image != null && admin_image.Length > 0)
            {
                // Create folder if it doesn't exist
                string folder = Path.Combine(_env.WebRootPath, "admin_image");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                // Generate a unique filename to avoid conflicts
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(admin_image.FileName);
                string filePath = Path.Combine(folder, uniqueFileName);

                // Save the uploaded file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await admin_image.CopyToAsync(stream);
                }

                // Find the admin and update the image path in the database
                var admin = _context.tbl_admin.FirstOrDefault(a => a.Id == Id);
                if (admin != null)
                {
                    // If the admin has an existing image, delete it from the server if it exists
                    if (!string.IsNullOrEmpty(admin.admin_image))
                    {
                        string oldImagePath = Path.Combine(folder, admin.admin_image);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);  // Delete the old image
                        }
                    }

                    // Update the admin's image path with the new unique filename
                    admin.admin_image = uniqueFileName;
                    _context.SaveChanges();
                }
            }

            // Redirect back to Profile page
            return RedirectToAction("Profile");
        }


        public IActionResult fetchcustomer()
        {
            var data = _context.tbl_customer.ToList();
            return View(data); // Looks for a view named 'fetchcustomer.cshtml'
        }
        public IActionResult customerDetails(int ID)
        {
            var data = _context.tbl_customer.FirstOrDefault(c => c.customer_Id == ID);
            return View(data);
        }


        public IActionResult DeletePermission(int id)
        {
            return View(_context.tbl_customer.FirstOrDefault(c => c.customer_Id == id));
        }

        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.tbl_customer.FirstOrDefault(c => c.customer_Id == id);
            if (customer != null)
            {
                _context.tbl_customer.Remove(customer);
                _context.SaveChanges();
            }
            return RedirectToAction("fetchcustomer");
        }
        public IActionResult fetchcategory()
        {
            return View(_context.tbl_category.ToList());
        }
        [HttpGet]
        public IActionResult addcategory()
        {
            return View(); // Renders the form
        }
        [HttpPost]
        public IActionResult addcategory(category cat)
        {
            _context.tbl_category.Add(cat);
            _context.SaveChanges();
            return View();
        }
        [HttpGet]
        public IActionResult categoryUpdate(int id)
        {
            var category = _context.tbl_category.Find(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult categoryUpdate(category cat)
        {
            _context.tbl_category.Update(cat);
            _context.SaveChanges();
            return RedirectToAction("fetchcategory");
        }
        public IActionResult DeletePermissioncategory(int id)
        {
            return View(_context.tbl_category.FirstOrDefault(c => c.category_Id == id));
        }
        public IActionResult deletecategory(int id)
        {
            var category = _context.tbl_category.Find(id);
            _context.tbl_category.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("fetchcategory");
        }
        public IActionResult fetchproduct()
        {
            return View(_context.tbl_product.ToList());
        }

        // Add product - GET
        [HttpGet]
        public IActionResult addproduct()
        {
            List<category> categories = _context.tbl_category.ToList();
            ViewData["category"] = categories;
            return View();
        }

        // Add product - POST
        [HttpPost]
        public IActionResult addproduct(product prod, IFormFile product_image)
        {
            if (product_image != null && product_image.Length > 0)
            {
                string folderPath = Path.Combine(_env.WebRootPath, "product_images");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string imageName = Path.GetFileName(product_image.FileName);
                string imagePath = Path.Combine(folderPath, imageName);

                using (var fs = new FileStream(imagePath, FileMode.Create))
                {
                    product_image.CopyTo(fs);
                }

                prod.product_image = imageName;
            }

            _context.tbl_product.Add(prod);
            _context.SaveChanges();

            return RedirectToAction("fetchproduct");
        }
        public IActionResult productdetails(int ID)
        {
            var data = _context.tbl_product.Include(p => p.category).FirstOrDefault(p => p.product_Id == ID);
            return View(data);
        }
        // Update product - GET
        [HttpGet]
        // GET: Load product into form
        public IActionResult productUpdate(int id)
        {
            List<category> categories = _context.tbl_category.ToList();
            ViewData["category"] = categories;

            var product = _context.tbl_product.Find(id);
            ViewBag.selectedCategory = product.cat_id;
            return View(product);
        }

        // POST: Update product data
        [HttpPost]
        public IActionResult productUpdate(product prod, IFormFile product_image)
        {
            var existingProduct = _context.tbl_product.Find(prod.product_Id);
            if (existingProduct == null)
                return NotFound();

            // Update fields
            existingProduct.product_name = prod.product_name;
            existingProduct.product_price = prod.product_price;
            existingProduct.product_description = prod.product_description;
            existingProduct.cat_id = prod.cat_id;

            // If new image is uploaded, save it and update
            if (product_image != null && product_image.Length > 0)
            {
                string imageName = Path.GetFileName(product_image.FileName);
                string imagePath = Path.Combine(_env.WebRootPath, "product_images", imageName);
                using (var fs = new FileStream(imagePath, FileMode.Create))
                {
                    product_image.CopyTo(fs);
                }
                existingProduct.product_image = imageName;
            }

            _context.SaveChanges();
            return RedirectToAction("fetchproduct");
        }


        // Confirm delete page
        public IActionResult DeletePermissionproduct(int id)
        {
            return View(_context.tbl_product.FirstOrDefault(p => p.product_Id == id));
        }

        // Final delete
        public IActionResult deleteproduct(int id)
        {
            var product = _context.tbl_product.Find(id);
            _context.tbl_product.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("fetchproduct");
        }
      
    }
}
