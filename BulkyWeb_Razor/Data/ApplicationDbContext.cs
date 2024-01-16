using BulkyWeb_Razor.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb_Razor.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Action", CategoryDisplayOrder = 1 },
                new Category { CategoryId = 2, CategoryName = "SciFi", CategoryDisplayOrder = 2 },
                new Category { CategoryId = 3, CategoryName = "History", CategoryDisplayOrder = 3 }
                );
        }
    }
}