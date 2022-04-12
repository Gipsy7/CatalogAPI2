using CatalogAPI2.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI2.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        public DbSet<Product>? Products { get; set; }
        public DbSet<Category>? Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //Category
            mb.Entity<Category>().HasKey(c => c.Id);
            mb.Entity<Category>().Property(c => c.Name).HasMaxLength(100).IsRequired();
            mb.Entity<Category>().Property(c => c.Description).HasMaxLength(300).IsRequired();

            //Product
            mb.Entity<Product>().HasKey(c => c.Id);
            mb.Entity<Product>().Property(c => c.Name).HasMaxLength(100).IsRequired();
            mb.Entity<Product>().Property(c => c.Description).HasMaxLength(300);
            mb.Entity<Product>().Property(c => c.Image).HasMaxLength(100);
            mb.Entity<Product>().Property(c => c.Price).HasPrecision(14, 2);

            //Relationship
            mb.Entity<Product>().HasOne<Category>(c => c.Category).WithMany(c => c.Products).HasForeignKey(c => c.CategoryId);
        }
    }
}
