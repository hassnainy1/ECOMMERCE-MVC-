using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace E_commerce_Website.Models
{
    public class product
    {
        [Key]
        public int product_Id { get; set; }

        public string product_name { get; set; }
        public string product_description { get; set; }
        public string product_price { get; set; }
        public string product_image { get; set; }
        public int cat_id
        {
            get; set;

        }
        public category category { get; set; }
    }
}

