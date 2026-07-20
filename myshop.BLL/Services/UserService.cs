using Microsoft.AspNetCore.Identity;
using myshop.BLL.DTOs.User;
using myshop.BLL.Interfaces;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services
{
    internal class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public Task<IEnumerable<UserReadDto>> GetAllAsync(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task PromoteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task DemoteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task LockAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UnlockAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
