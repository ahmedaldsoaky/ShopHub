using AutoMapper;
using myshop.BLL.DTOs.User;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Mapping.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Account
            CreateMap<ApplicationUser, UserReadDto>();
        }
    }
}
