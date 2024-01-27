using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductsController : Controller
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public ProductsController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)

		{
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
		}

		public IActionResult Index()
		{
			List<Product> objProductsList = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category").ToList();
			return View(objProductsList);
		}

		public IActionResult Upsert(int? productId)
		{
			//ViewBag.CategoryList = CategoryList;
			//ViewData["CategoryList"] = CategoryList;
			ProductVM productVM = new()
			{
				CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(u => new SelectListItem
				{
					Text = u.CategoryName,
					Value = u.CategoryId.ToString(),
				}),
				Product = new Product()
			};

			if (productId == null || productId == 0)
			{
				//Create
				return View(productVM);
			}
			else
			{
				//Update
				productVM.Product = _unitOfWork.ProductRepository.Get(u => u.ProductId == productId);
				return View(productVM);
			}
		}

		[HttpPost]
		public IActionResult Upsert(ProductVM productVM, IFormFile? file)
		{

			if (ModelState.IsValid)
			{
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string productPath = Path.Combine(wwwRootPath, @"images\product");

					if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
					{
						//delete the old image
						var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}

					}

					using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					productVM.Product.ImageUrl = @"\images\product\" + fileName;
				}

				if (productVM.Product.ProductId == 0)
				{
					_unitOfWork.ProductRepository.Add(productVM.Product);
				}
				else
				{
					_unitOfWork.ProductRepository.Update(productVM.Product);
				}

				_unitOfWork.Save();
				TempData["Success"] = "Product created successfully";
				return RedirectToAction("Index", "Products");
			}
			else
			{

				productVM.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(u => new SelectListItem
				{
					Text = u.CategoryName,
					Value = u.CategoryId.ToString(),
				});
			}
			return View(productVM);
		}

		public IActionResult Delete(int? ProductId)
		{

			if (ProductId == null || ProductId == 0)
			{
				return NotFound();
			}

			Product? product = _unitOfWork.ProductRepository.Get(p => p.ProductId == ProductId);

			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		[HttpPost]
		[ActionName("Delete")]
		public IActionResult DeletePost(int? ProductId)
		{
			if (ProductId == null || ProductId == 0)
			{
				return NotFound();
			}

			Product? product = _unitOfWork.ProductRepository.Get(p => p.ProductId == ProductId);

			if (product == null)
			{
				return NotFound();
			}

			//Delete img in wwwroot when delete product in database
			string wwwRootPath = _webHostEnvironment.WebRootPath;
			var oldImagePath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('\\'));
			if (System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}
			//End

			_unitOfWork.ProductRepository.Remove(product);
			_unitOfWork.Save();
			TempData["Success"] = "Product deleted successfully";

			return RedirectToAction("Index", "Products");
		}

		#region API CALLS
		[HttpGet]
		public IActionResult GetALl(int productId)
		{
			List<Product> objProductsList = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category").ToList();
			return Json(new { data = objProductsList });
		}
		#endregion

	}
}
