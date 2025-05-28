using System.ComponentModel.DataAnnotations;

namespace E_commerce_Website.Models
{
    public class category

    {
        [Key]
        public int category_Id { get; set; }

        public string category_Name { get; set; }

        public List<product> products { get; set; }
    }
}
