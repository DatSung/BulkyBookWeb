using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using BulkyWeb.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext _db;

		public CategoryRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

        public void Save()
		{
			_db.SaveChanges();
		}

		public void Update(Category category)
		{
			_db.Categories.Update(category);
		}
	}
}
