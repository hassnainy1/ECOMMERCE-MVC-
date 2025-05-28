using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce_Website.Models
{

    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }

        public string Status { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
