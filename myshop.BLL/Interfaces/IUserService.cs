using myshop.BLL.DTOs.User;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Interfaces
{
    public interface IUserService : IBaseService<UserReadDto, ApplicationUser, string>
    {
        Task<bool> PromoteAsync(string id);

        Task<bool> DemoteAsync(string id, ClaimsPrincipal User);

        Task<bool> LockAsync(string id, ClaimsPrincipal User);

        Task<bool> DeleteAsync(string id, ClaimsPrincipal User);
        Task<bool> UnlockAsync(string id);
    }
}
