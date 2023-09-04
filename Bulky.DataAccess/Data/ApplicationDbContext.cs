using Bulky.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Bulky.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );

            modelBuilder.Entity<Product>().HasData(
               new Product
               {
                   Id = 1,
                   Title = "Origin",
                   Description = "Origin of religion",
                   ISBN = "SWD9999001",
                   Author = "Dan brown",
                   Price = 50,
                   Price50 = 45,
                   Price100 = 40,
                   CategoryId = 1
               },
               new Product
               {
                   Id = 2,
                   Title = "Wars guns and votes",
                   Description = "democrasy in dengarous places",
                   ISBN = "ERD559900",
                   Author = "gaddafi",
                   Price = 35,
                   Price50 = 32,
                   Price100 = 30,
                   CategoryId = 2
               },
                new Product
                {
                    Id = 3,
                    Title = "Rich dad poor dad",
                    Description = "phsycological improvment",
                    ISBN = "XC156156E",
                    Author = "ibrahim",
                    Price = 10.5,
                    Price50 = 9,
                    Price100 = 7.75,
                    CategoryId = 3
                }
                );
        }
    }
}
