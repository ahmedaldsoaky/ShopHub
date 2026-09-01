using AutoMapper;
using myshop.BLL.DTOs.Product;
using myshop.Entities.Models;

namespace myshop.BLL.Mapping.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Products
            CreateMap<ProductCreateDto, Product>();

            CreateMap<ProductUpdateDto, Product>()
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Product, ProductUpdateDto>();

            CreateMap<Product, ProductReadDto>()
               .ForMember(d => d.CategoryName,
               opt => opt.MapFrom(s => s.Category!.Name));
        }
    }
}
