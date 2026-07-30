using AutoMapper;
using myshop.BLL.DTOs.Category;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Mapping.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            // Categories
            CreateMap<CategoryCreateDto, Category>();

            CreateMap<CategoryUpdateDto, Category>();

            CreateMap<Category, CategoryReadDto>();
            CreateMap<Category, CategoryUpdateDto>();
        }
    }
}
