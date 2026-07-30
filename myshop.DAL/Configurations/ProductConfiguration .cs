using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myshop.Entities.Models;

namespace myshop.DAL.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Price)
                   .HasPrecision(18, 2);

            //builder.HasData(
            //    new Product
            //    {
            //        Id = 1,
            //        Name = "Laptop Lenovo",
            //        Description = "Lenovo IdeaPad 5",
            //        ImgPath = "Images/Products/laptop.jpg",
            //        Price = 25000,
            //        CategoryId = 1
            //    },
            //    new Product
            //    {
            //        Id = 2,
            //        Name = "Wireless Mouse",
            //        Description = "Logitech Wireless Mouse",
            //        ImgPath = "Images/Products/mouse.jpg",
            //        Price = 600,
            //        CategoryId = 1
            //    },
            //    new Product
            //    {
            //        Id = 3,
            //        Name = "Men T-Shirt",
            //        Description = "100% Cotton",
            //        ImgPath = "Images/Products/tshirt.jpg",
            //        Price = 350,
            //        CategoryId = 2
            //    },
            //    new Product
            //    {
            //        Id = 4,
            //        Name = "Clean Code",
            //        Description = "Robert C. Martin",
            //        ImgPath = "Images/Products/cleancode.jpg",
            //        Price = 850,
            //        CategoryId = 3
            //    },
            //    new Product
            //    {
            //        Id = 5,
            //        Name = "Coffee Maker",
            //        Description = "Automatic Coffee Machine",
            //        ImgPath = "Images/Products/coffee.jpg",
            //        Price = 3200,
            //        CategoryId = 4
            //    }
            //);
        }
    }
}
