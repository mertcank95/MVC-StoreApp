using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.ProductName).IsRequired();
            builder.Property(p => p.Price).IsRequired();
            builder.HasData(
                    new Product() { ProductId = 1, CategoryId = 1,ImageUrl="/images/1.jpg", ProductName = "Computer", Price = 17000,ShowCase=false },
                    new Product() { ProductId = 2, CategoryId = 2,ImageUrl="/images/2.jpg", ProductName = "KeyBoard", Price = 1000,ShowCase=false },
                    new Product() { ProductId = 3, CategoryId = 2,ImageUrl="/images/3.jpg", ProductName = "Mouse", Price = 5000,ShowCase=false },
                    new Product() { ProductId = 4, CategoryId = 2,ImageUrl="/images/4.jpg", ProductName = "Monitor", Price = 7200,ShowCase=false },
                    new Product() { ProductId = 5, CategoryId = 2,ImageUrl="/images/5.jpg", ProductName = "Deck", Price = 1500,ShowCase=false },
                    new Product() { ProductId = 6, CategoryId = 1,ImageUrl="/images/6.jpg", ProductName = "History", Price = 350,ShowCase=false },
                    new Product() { ProductId = 7, CategoryId = 2,ImageUrl="/images/4.jpg", ProductName = "Hamlet", Price = 854,ShowCase=false },
                    new Product() { ProductId = 8, CategoryId = 1,ImageUrl="/images/3.jpg", ProductName = "Xp-Pen", Price = 235,ShowCase=true },
                    new Product() { ProductId = 9, CategoryId = 2,ImageUrl="/images/2.jpg", ProductName = "Samsung Glaxy Fe", Price = 4500,ShowCase=true },
                    new Product() { ProductId = 10, CategoryId = 1,ImageUrl="/images/1.jpg", ProductName = "Hp - Mouse", Price = 1000,ShowCase=true }
            );




        }
    }
}
