using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;  // For the collection of Orders

namespace E_commerce_Website.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }  // Cart ID

        [ForeignKey("Product")]
        public int ProId { get; set; }  // Foreign key to Product

        [ForeignKey("Customer")]
        public int CustId { get; set; }  // Foreign key to Customer

        public int ProductQuantity { get; set; }  // Quantity of the product in the cart
        public int CartStatus { get; set; }  // Cart status (e.g., 0: Pending, 1: Completed)

        // Navigation properties
        public virtual product Product { get; set; }
        public virtual customer Customer { get; set; }

        // Add the navigation property for Orders
        public virtual ICollection<Order> Orders { get; set; }  // One-to-many relationship with Order
    }
}
