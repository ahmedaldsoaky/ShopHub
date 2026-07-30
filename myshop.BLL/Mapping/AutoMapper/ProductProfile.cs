using AutoMapper;
using myshop.BLL.DTOs.Product;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
