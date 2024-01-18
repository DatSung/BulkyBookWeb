using BulkyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb.Controllers
{
	public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
		public IActionResult Create(Category category)
		{
            //if (category.CategoryName == category.CategoryDisplayOrder.ToString()) 
            //{
            //    ModelState.AddModelError("CategoryName", "The DisplayOrder cannot exactly match the Name");
            //}

            if (category.CategoryName.All(char.IsDigit))
            {
                ModelState.AddModelError("CategoryName", "Category Name can not be a number");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["Success"] = "Category created successfully";
				return RedirectToAction("Index", "Category");
			}
            return View();
		}

     

		public IActionResult Edit(int? CategoryId)
		{
            if(CategoryId == null || CategoryId == 0)
            {
                return NotFound();
            }

            Category? category = _db.Categories.Find(CategoryId);
            //Category? category1 = _db.Categories.FirstOrDefault(x => x.CategoryId == CategoryId);
            //Category? category2 = _db.Categories.Where(x => x.CategoryId == CategoryId).FirstOrDefault();

			if (category == null) 
            {
                return NotFound();
            }

			return View(category);
		}

		[HttpPost]
		public IActionResult Edit(Category category)
		{
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
				TempData["Success"] = "Category updated successfully";
				return RedirectToAction("Index");
			}

			return View();
		}


        public IActionResult Delete(int? CategoryId)
        {

			if (CategoryId == null || CategoryId == 0)
			{
				return NotFound();
			}

			Category? category = _db.Categories.Find(CategoryId);
			//Category? category1 = _db.Categories.FirstOrDefault(x => x.CategoryId == CategoryId);
			//Category? category2 = _db.Categories.Where(x => x.CategoryId == CategoryId).FirstOrDefault();

			if (category == null)
			{
				return NotFound();
			}

			return View(category);
		}

        [HttpPost]
        [ActionName("Delete")]
		public IActionResult DeletePost(int? CategoryId)
		{
			if (CategoryId == null || CategoryId == 0)
			{
				return NotFound();
			}

			Category? category = _db.Categories.Find(CategoryId);
			//Category? category1 = _db.Categories.FirstOrDefault(x => x.CategoryId == CategoryId);
			//Category? category2 = _db.Categories.Where(x => x.CategoryId == CategoryId).FirstOrDefault();

			if (category == null)
			{
				return NotFound();
			}

            _db.Categories.Remove(category);
            _db.SaveChanges();
			TempData["Success"] = "Category deleted successfully";

			return RedirectToAction("Index");
		}

	}
}
