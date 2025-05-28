using System.ComponentModel.DataAnnotations;

namespace E_commerce_Website.Models
{
    public class faqs
    {
        [Key]
        public int faq_Id { get; set; }

        public string faq_Ques { get; set; }
        public string faq_Answer { get; set; }
    }
}
