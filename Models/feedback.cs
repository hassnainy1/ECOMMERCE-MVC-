using System.ComponentModel.DataAnnotations;

namespace E_commerce_Website.Models
{
    public class feedback
    {
        [Key]
        public int feedback_Id { get; set; }

        public string user_Name { get; set; }
        public string user_Message { get; set; }
    }
}
