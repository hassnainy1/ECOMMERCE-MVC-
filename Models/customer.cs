using System.ComponentModel.DataAnnotations;

namespace E_commerce_Website.Models
{
    public class customer
    {
        [Key]
        public int customer_Id { get; set; }

        public string customer_name { get; set; }
        public string customer_phone { get; set; }

        public string customer_eamil { get; set; }
        public string customer_password { get; set; }

        public string customer_image { get; set; }
        public string customer_country { get; set; }
        public string customer_city { get; set; }
        
        public string customer_gender { get; set; }
      
        public string customer_address { get; set; }
    }
}
