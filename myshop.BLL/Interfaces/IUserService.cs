using myshop.BLL.DTOs.User;
using myshop.Entities.Models;
using System.Security.Claims;

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
