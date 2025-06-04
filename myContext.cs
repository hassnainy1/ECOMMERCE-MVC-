using Microsoft.EntityFrameworkCore;


namespace E_commerce_Website.Models
{
    public class myContext : DbContext
    { public myContext(DbContextOptions<myContext> options) : base(options) { }
    


    public DbSet<Admin> tbl_admin { get; set; }
        public DbSet<customer> tbl_customer { get; set; }
        public DbSet<category> tbl_category { get; set; } 
        public DbSet<product> tbl_product { get; set; }

        public DbSet<Cart> tbl_cart { get; set; }

        public DbSet<feedback> tbl_feedback { get; set; }
        public DbSet<faqs> tbl_faqs { get; set; }

        public DbSet<Order> tbl_order{ get; set; }  // DbSet for Order entity

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationship between Product and Category
            modelBuilder.Entity<product>()
                .HasOne(p => p.category)
                .WithMany(c => c.products)
                .HasForeignKey(p => p.cat_id);

            // Define relationship between Order and Cart
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Cart)  // Order has one Cart
                .WithMany(c => c.Orders)  // Cart has many Orders
                .HasForeignKey(o => o.CartId);  // Foreign key in Order class

            // Add any other relationships you may have here

            base.OnModelCreating(modelBuilder);  // Always call the base method
        }

    }
}