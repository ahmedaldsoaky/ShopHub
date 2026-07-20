using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "Electronics",
                    Description = "Electronic devices and accessories",
                    CreatedTime = new DateTime(2026, 1, 1)
                },
                new Category
                {
                    Id = 2,
                    Name = "Fashion",
                    Description = "Clothing and accessories",
                    CreatedTime = new DateTime(2026, 1, 1)
                },
                new Category
                {
                    Id = 3,
                    Name = "Books",
                    Description = "Books and educational materials",
                    CreatedTime = new DateTime(2026, 1, 1)
                },
                new Category
                {
                    Id = 4,
                    Name = "Home",
                    Description = "Home and kitchen products",
                    CreatedTime = new DateTime(2026, 1, 1)
                }
            );
        }
    }
}
