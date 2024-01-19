using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }

		[Required]
		[MaxLength(30)]
		[DisplayName("Category Name")]
		public string CategoryName { get; set; }

		[Required]
		[DisplayName("Display Order")]
		[Range(1, 100)]
		public int CategoryDisplayOrder { get; set; }
	}
}
