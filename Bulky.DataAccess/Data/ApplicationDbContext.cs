using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.DataAccess.Data
{
	public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }

    }
}