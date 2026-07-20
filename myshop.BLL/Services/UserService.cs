using AutoMapper;
using Microsoft.AspNetCore.Identity;
using myshop.BLL.DTOs.User;
using myshop.BLL.Interfaces;
using myshop.Common;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserReadDto>> GetAllAsync()
        {
            var users = _userManager.Users.ToList();
            var usersDtos = new List<UserReadDto>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = _mapper.Map<UserReadDto>(user);
                userDto.Role = roles.FirstOrDefault()!;
                userDto.IsLocked = user.LockoutEnd.HasValue &&
                        user.LockoutEnd > DateTimeOffset.UtcNow;
                usersDtos.Add(userDto);
            }
            return usersDtos;
        }

        public async Task<bool> PromoteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            await _userManager.RemoveFromRoleAsync(user, Roles.Customer);

            await _userManager.AddToRoleAsync(user, Roles.Admin);
            return true;
        }

        public async Task<bool> DemoteAsync(string id, string currentUserId)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            if (user.Id == currentUserId)
                return false;


            await _userManager.RemoveFromRoleAsync(user, Roles.Admin);

            await _userManager.AddToRoleAsync(user, Roles.Customer);
            return true;
        }

        public async Task<bool> LockAsync(string id, string currentUserId)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            if (user.Id == currentUserId)
                return false;

            user.LockoutEnd = DateTimeOffset.MaxValue;
            await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task<bool> UnlockAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            user.LockoutEnd = null;

            await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeleteAsync(string id, string currentUserId)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;
            if (user.Id == currentUserId)
                return false;
            await _userManager.DeleteAsync(user);
            return true;
        }
    }
}
