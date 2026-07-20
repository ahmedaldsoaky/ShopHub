using AutoMapper;
//using AutoMapper;
using myshop.BLL.DTOs.Category;
using myshop.BLL.DTOs.Product;
using myshop.BLL.DTOs.User;
using myshop.Entities.Models;

namespace myshop.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Products
            CreateMap<ProductCreateDto, Product>();

            CreateMap<ProductUpdateDto, Product>();

            CreateMap<Product, ProductReadDto>()
               .ForMember(d => d.CategoryName,
               opt => opt.MapFrom(s => s.Category!.Name));

            CreateMap<Product, ProductUpdateDto>();

            // Categories
            CreateMap<CategoryCreateDto, Category>();

            CreateMap<CategoryUpdateDto, Category>();

            CreateMap<Category, CategoryReadDto>();
            CreateMap<Category, CategoryUpdateDto>();


            // Account

            CreateMap<ApplicationUser, UserReadDto>();
        }
    }
}
