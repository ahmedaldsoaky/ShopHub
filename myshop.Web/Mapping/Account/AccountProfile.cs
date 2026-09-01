using AutoMapper;
using Microsoft.Win32;
using myshop.BLL.DTOs.Account;
using myshop.Web.ViewModels.Account;

namespace myshop.Web.Mapping.Account
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<LoginVM, LoginDto>();
            CreateMap<LoginDto, LoginVM>();
            CreateMap<RegisterVM, RegisterDto>();
            CreateMap<RegisterDto, RegisterVM>();
        }
    }
}
