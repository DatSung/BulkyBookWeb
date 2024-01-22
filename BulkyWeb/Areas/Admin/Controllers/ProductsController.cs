using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(IUnitOfWork unitOfWork)

        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> objProductsList = _unitOfWork.ProductRepository.GetAll().ToList();
            return View(objProductsList);
        }
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Product product)
		{

			if (ModelState.IsValid)
			{
				_unitOfWork.ProductRepository.Add(product);
				_unitOfWork.Save();
				TempData["Success"] = "Product created successfully";
				return RedirectToAction("Index", "Products");
			}
			return View();
		}


		public IActionResult Edit(int? ProductId)
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
		public IActionResult Edit(Product product)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.ProductRepository.Update(product);
				_unitOfWork.Save();
				TempData["Success"] = "Product updated successfully";
				return RedirectToAction("Index");
			}

			return View();
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

			_unitOfWork.ProductRepository.Remove(product);
			_unitOfWork.Save();
			TempData["Success"] = "Product deleted successfully";

			return RedirectToAction("Index", "Products");
		}
	}
}
