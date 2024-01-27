using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBookWeb.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var productFromDb = _db.Products.FirstOrDefault(u => u.ProductId == product.ProductId);
            if (productFromDb != null)
            {
                productFromDb.ProductTitle = product.ProductTitle;
                productFromDb.ISBN = product.ISBN;
                productFromDb.Price = product.Price;
                productFromDb.Price50 = product.Price50;
                productFromDb.Price100 = product.Price100;
                productFromDb.ListPrice = product.ListPrice;
                productFromDb.ProductDescription = product.ProductDescription;
                productFromDb.CategoryId = product.CategoryId;
                productFromDb.Author = product.Author;

                if (productFromDb.ImageUrl != null) 
                {
                    productFromDb.ImageUrl = product.ImageUrl;
                }

			}
        }
    }
}
