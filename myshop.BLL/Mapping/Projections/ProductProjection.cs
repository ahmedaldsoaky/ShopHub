using myshop.BLL.DTOs.Product;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Mapping.Projections
{
    public static class ProductProjection
    {
        public static readonly Expression<Func<Product, ProductReadDto>> ToReadDto =
            p => new ProductReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                ImgPath = p.ImgPath,
                Price = p.Price,
                CategoryName = p.Category.Name ?? "No Category",
            };
    }
}
