using AutoMapper;
//using AutoMapper;
using myshop.BLL.DTOs.Category;
using myshop.BLL.DTOs.Product;
using myshop.Entities.Models;
using myshop.Web.ViewModels.Category;
using myshop.Web.ViewModels.Product;

namespace myshop.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Products
            CreateMap<ProductCreateVM, ProductCreateDto>()
            .ForMember(dest => dest.ImgPath,
                       opt => opt.Ignore());

            CreateMap<ProductUpdateDto, Product>();
            
            CreateMap<ProductUpdateDto, ProductUpdateVM>()
                .ForMember(dest => dest.Img,
                       opt => opt.Ignore());

            CreateMap<ProductUpdateVM, ProductUpdateDto>()
                .ForMember(dest => dest.ImgPath,
                       opt => opt.Ignore());

            CreateMap<Product, ProductReadDto>();

            CreateMap<ProductReadDto, ProductUpdateVM>()
                .ForMember(d => d.Img, opt => opt.Ignore())
                .ForMember(d => d.CategoryList, opt => opt.Ignore());


            //Categories
            CreateMap<CategoryCreateDto, CategoryCreateVM>();
            CreateMap<CategoryCreateVM, CategoryCreateDto>();
            
            CreateMap<CategoryUpdateVM, CategoryUpdateDto>();
            CreateMap<CategoryUpdateDto, CategoryUpdateVM>();

            CreateMap<CategoryReadDto, CategoryUpdateVM>();
            CreateMap<CategoryUpdateVM, CategoryReadDto>();


            //CreateMap<CategoryUpdateDto, Category>();

            //CreateMap<Category, CategoryReadDto>();
        }
    }
}
