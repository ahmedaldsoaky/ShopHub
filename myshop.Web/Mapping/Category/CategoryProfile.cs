using AutoMapper;
using myshop.BLL.DTOs.Category;
using myshop.Web.ViewModels.Category;

namespace myshop.Web.Mapping.Category
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            //Categories
            CreateMap<CategoryCreateDto, CategoryCreateVM>();
            CreateMap<CategoryCreateVM, CategoryCreateDto>();

            CreateMap<CategoryUpdateVM, CategoryUpdateDto>();
            CreateMap<CategoryUpdateDto, CategoryUpdateVM>();

            CreateMap<CategoryReadDto, CategoryUpdateVM>();
            CreateMap<CategoryUpdateVM, CategoryReadDto>();
        }
    }
}
