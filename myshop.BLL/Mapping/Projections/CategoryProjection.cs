using myshop.BLL.DTOs.Category;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Mapping.Projections
{
    public class CategoryProjection
    {
        public static readonly Expression<Func<Category, CategoryReadDto>> ToReadDto =
            c => new CategoryReadDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                CreatedTime = c.CreatedTime
            };
    }
}
