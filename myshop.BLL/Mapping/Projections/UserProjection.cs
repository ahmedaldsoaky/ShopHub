using myshop.BLL.DTOs.Product;
using myshop.BLL.DTOs.User;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Mapping.Projections
{
    public class UserProjection
    {
        public static readonly Expression<Func<ApplicationUser, UserReadDto>> ToReadDto =
            u => new UserReadDto
            {
                Id = u.Id,
                UserName = u.UserName!,
                FullName = u.FullName,
                Email = u.Email!,
                IsLocked = u.LockoutEnd != null && 
                            u.LockoutEnd > DateTimeOffset.UtcNow,
            };
    }
}