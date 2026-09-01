using AutoMapper;
using myshop.BLL.DTOs.Checkout;
using myshop.Web.ViewModels.Checkout;

namespace myshop.Web.Mapping.Checkout
{
    public class CheckoutProfile : Profile
    {
        public CheckoutProfile()
        {
            CreateMap<CheckoutVM, CheckoutDto>();
        }
    }
}
