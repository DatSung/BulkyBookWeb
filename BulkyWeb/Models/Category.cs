using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        [Required]
        [DisplayName("Display Order")]
        public int CategoryDisplayOrder { get; set; }
    }
}
