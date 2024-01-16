using BulkyWeb_Razor.Data;
using BulkyWeb_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb_Razor.Pages.Categories
{
    public class EditModel : PageModel
    {
		private readonly ApplicationDbContext _db;

		[BindProperty]//Khai báo để liên kết các giá trị nhập từ form thành object Category
					  //Đôi khi không cần khai báo thì nó vãn thử tự động liên kết, được thì bú không thì thôi
					  //Nếu có nhiều object nhưng lười khai báo thì có thể khai báo 1 lần cho tất cả ở bên ngoài class
		public Category Category { get; set; }


		public EditModel(ApplicationDbContext db)
		{
			_db = db;
		}
		public void OnGet(int categoryId)
		{
			if (categoryId != null && categoryId != 0)
			{
				Category = _db.Categories.Find(categoryId);
			}
		}

		public IActionResult OnPost()
		{
			if (Category.CategoryName.All(char.IsDigit))
			{
				ModelState.AddModelError("Category.CategoryName", "The Category Name can not be a number");
			}

			if (ModelState.IsValid)
			{
				_db.Categories.Update(Category);
				_db.SaveChanges();
                TempData["Success"] = "Category updated successfully";
                return RedirectToPage("Index");
			}
			return Page();

		}
	}
}
