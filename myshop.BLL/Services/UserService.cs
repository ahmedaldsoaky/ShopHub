using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using myshop.BLL.DTOs.User;
using myshop.BLL.Interfaces;
using myshop.Common;
using myshop.DAL.Context;
using myshop.Entities.Models;
using System.Linq.Expressions;
using System.Security.Claims;

namespace myshop.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext context;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            this.context = context;
        }

        // get all -> infinit loop  => not implemented
        public async Task<IEnumerable<UserReadDto>> GetAllAsync()
        {
            var query = context.Users
                .Join(
                    context.UserRoles,
                    u => u.Id,
                    ur => ur.UserId,
                    (u, ur) => new { u, ur })
                .Join(
                    context.Roles,
                    x => x.ur.RoleId,
                    r => r.Id,
                    (x, r) => new UserReadDto
                    {
                        Id = x.u.Id,
                        UserName = x.u.UserName!,
                        FullName = x.u.FullName,
                        Email = x.u.Email!,
                        PhoneNumber = x.u.PhoneNumber!,
                        IsLocked = x.u.LockoutEnd != null && x.u.LockoutEnd > DateTimeOffset.UtcNow,
                        Role = r.Name!
                    })
                .OrderBy(x => x.FullName); // Default ordering

            return await query.ToListAsync();
        }

        public async Task<PagedResult<UserReadDto>> GetPagedAsync(PagedRequestDto requestDto)
        {
            Expression<Func<ApplicationUser, bool>>? filter = null;
            
            if(!string.IsNullOrWhiteSpace(requestDto?.Search))
            {
                string searchTerm = requestDto.Search.Trim();
                filter = 
                        f =>
                            f.FullName.Contains(searchTerm) ||
                            f.UserName!.Contains(searchTerm) ||
                            f.PhoneNumber!.Contains(searchTerm) ||
                            f.Email!.Contains(searchTerm);
            }
            
            Func<IQueryable<UserReadDto>, IOrderedQueryable<UserReadDto>>? orderBy = null;

            switch(requestDto?.SortColumn?.ToLower())
            {
                case "fullname":
                    orderBy = requestDto.SortDirection?.ToLower() == "desc"
                        ? q => q.OrderByDescending(f => f.FullName)
                        : q => q.OrderBy(f => f.FullName);
                    break;
                case "username":
                    orderBy = requestDto.SortDirection?.ToLower() == "desc"
                        ? q => q.OrderByDescending(f => f.UserName)
                        : q => q.OrderBy(f => f.UserName);
                    break;
                case "email":
                    orderBy = requestDto.SortDirection?.ToLower() == "desc"
                        ? q => q.OrderByDescending(f => f.Email)
                        : q => q.OrderBy(f => f.Email);
                    break;
                case "phonenumber":
                    orderBy = requestDto.SortDirection?.ToLower() == "desc"
                        ? q => q.OrderByDescending(f => f.PhoneNumber)
                        : q => q.OrderBy(f => f.PhoneNumber);
                    break;
                case "role":
                    orderBy = requestDto.SortDirection?.ToLower() == "desc"
                        ? q => q.OrderByDescending(f => f.Role)
                        : q => q.OrderBy(f => f.Role);
                    break;
                case "islocked":
                    orderBy = requestDto.SortDirection?.ToLower() == "desc"
                        ? q => q.OrderByDescending(f => f.IsLocked)
                        : q => q.OrderBy(f => f.IsLocked);
                    break;

                default:
                    orderBy = q => q.OrderBy(x => x.FullName);
                    break;
            }

            var query = context.Users
                .Where(filter ?? (f => true)) // Apply search filter if provided
                .Join(
                    context.UserRoles,
                    u => u.Id,
                    ur => ur.UserId,
                    (u, ur) => new { u, ur })
                .Join(
                    context.Roles,
                    x => x.ur.RoleId,
                    r => r.Id,
                    (x, r) => new UserReadDto
                    {
                        Id = x.u.Id,
                        UserName = x.u.UserName!,
                        FullName = x.u.FullName,
                        Email = x.u.Email!,
                        PhoneNumber = x.u.PhoneNumber!,
                        IsLocked = x.u.LockoutEnd != null &&
                                    x.u.LockoutEnd > DateTimeOffset.UtcNow,
                        Role = r.Name!
                    });

            // Apply sorting
            query = orderBy(query);
            
            var filteredCount = await query.CountAsync();

            var users = await query
                .Skip((requestDto.PageNumber - 1) * requestDto.PageSize)
                .Take(requestDto.PageSize)
                .ToListAsync();

            return new PagedResult<UserReadDto>
            {
                Data = users,
                TotalCount = await context.Users.CountAsync(),
                FilteredCount = filteredCount,
            };
        }

        public async Task<UserReadDto?> GetByIdAsync(string id)
        {
            var user = await context.Users
                .Where(u => u.Id == id)
                .Join(
                    context.UserRoles,
                    u => u.Id,
                    ur => ur.UserId,
                    (u, ur) => new { u, ur })
                .Join(
                    context.Roles,
                    x => x.ur.RoleId,
                    r => r.Id,
                    (x, r) => new UserReadDto
                    {
                        Id = x.u.Id,
                        UserName = x.u.UserName!,
                        FullName = x.u.FullName,
                        Email = x.u.Email!,
                        IsLocked = x.u.LockoutEnd != null && 
                                    x.u.LockoutEnd > DateTimeOffset.UtcNow,
                        Role = r.Name!
                    })
                .SingleOrDefaultAsync();

            return user;
        }

        public Task<bool> Exists(Expression<Func<ApplicationUser, bool>> expr)
            => _userManager.Users.AnyAsync(expr);

        public Task<int> CountAsync(Expression<Func<ApplicationUser, bool>>? filter = null)
        {
            if(filter is null)
                return _userManager.Users.CountAsync();
            return _userManager.Users.CountAsync(filter);
        }

        public async Task<bool> PromoteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            await _userManager.RemoveFromRoleAsync(user, Roles.Customer);

            await _userManager.AddToRoleAsync(user, Roles.Admin);
            return true;
        }

        public async Task<bool> DemoteAsync(string id, ClaimsPrincipal User)
        {
            var currentUserId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            if (user.Id == currentUserId)
                return false;


            await _userManager.RemoveFromRoleAsync(user, Roles.Admin);

            await _userManager.AddToRoleAsync(user, Roles.Customer);
            return true;
        }

        public async Task<bool> LockAsync(string id, ClaimsPrincipal User)
        {
            var currentUserId = _userManager.GetUserId(User);
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

        public async Task<bool> DeleteAsync(string id, ClaimsPrincipal User)
        {
            var currentUserId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;
            if (user.Id == currentUserId)
                return false;
            var res = await _userManager.DeleteAsync(user);

            return res.Succeeded;
        }
    }
}
