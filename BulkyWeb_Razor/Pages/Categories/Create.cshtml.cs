﻿using BulkyWeb_Razor.Data;
using BulkyWeb_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyWeb_Razor.Pages.Categories
{
    [BindProperties] //Khai bao 1 lan cho tat ca
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]//Khai báo để liên kết các giá trị nhập từ form thành object Category
        //Đôi khi không cần khai báo thì nó vãn thử tự động liên kết, được thì bú không thì thôi
        //Nếu có nhiều object nhưng lười khai báo thì có thể khai báo 1 lần cho tất cả ở bên ngoài class
        public Category Category { get; set; }


        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (Category.CategoryName.All(char.IsDigit)) 
            {
                ModelState.AddModelError("Category.CategoryName", "The Category Name can not be a number");
            }

            if (ModelState.IsValid)
            {
				_db.Categories.Add(Category);
				_db.SaveChanges();
                TempData["Success"] = "Category created successfully";
				return RedirectToPage("Index");
			}
            return Page();

        }
    }
}
