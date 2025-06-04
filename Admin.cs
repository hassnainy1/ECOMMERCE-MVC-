using System.ComponentModel.DataAnnotations;

namespace E_commerce_Website.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        public string admin_name { get; set; }
        public string admin_email { get; set; }
        public string admin_password { get; set; }
        public string admin_image { get; set; }

    }
}
