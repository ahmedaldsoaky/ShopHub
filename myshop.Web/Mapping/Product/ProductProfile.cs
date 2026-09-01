using AutoMapper;
using myshop.BLL.DTOs.Product;
using myshop.Web.ViewModels.Product;

namespace myshop.Web.Mapping.Product
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Products
            CreateMap<ProductCreateVM, ProductCreateDto>();

            CreateMap<ProductUpdateDto, ProductUpdateVM>()
                .ForMember(dest => dest.Img,
                       opt => opt.Ignore());

            CreateMap<ProductUpdateVM, ProductUpdateDto>();

            CreateMap<ProductReadDto, ProductUpdateVM>()
                .ForMember(d => d.Img, opt => opt.Ignore())
                .ForMember(d => d.CategoryList, opt => opt.Ignore());
        }
    }
}
