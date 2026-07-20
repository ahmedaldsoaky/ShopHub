using myshop.BLL.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAllAsync();

        Task<bool> PromoteAsync(string id);

        Task<bool> DemoteAsync(string id, string currentUserId);

        Task<bool> LockAsync(string id, string currentUserId);

        Task<bool> UnlockAsync(string id);
        Task<bool> DeleteAsync(string id, string currentUserId);
    }
}
