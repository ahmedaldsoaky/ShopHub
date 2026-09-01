using Microsoft.AspNetCore.Identity;
using myshop.BLL.DTOs.Account;
using myshop.BLL.Interfaces;
using myshop.Common;
using myshop.Entities.Models;

namespace myshop.BLL.Services
{
    internal class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<LoginResultDto> LoginAsync(LoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(
                dto.UserName,
                dto.Password,
                dto.RememberMe,
                lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                return new LoginResultDto
                {
                    Succeeded = false,
                    IsLockedOut = true,
                    Errors = new[]
                    {
                        "Your account has been locked due to multiple failed login attempts."
                    }
                };
            }

            if (!result.Succeeded)
            {
                return new LoginResultDto
                {
                    Succeeded = false,
                    Errors = new[]
                    {
                        "Invalid username or password."
                    }
                };
            }

            var user = await _userManager.FindByNameAsync(dto.UserName);

            if (user is null)
            {
                return new LoginResultDto
                {
                    Succeeded = false,
                    Errors = new[]
                    {
                        "User not found."
                    }
                };
            }

            var role = await GetUserRoleAsync(user);

            return new LoginResultDto
            {
                Succeeded = true,
                Role = role
            };
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<RegisterResultDto> RegisterAsync(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                FullName = dto.FullName,
                UserName = dto.UserName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return new RegisterResultDto
                {
                    Succeeded = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            await _userManager.AddToRoleAsync(user, Roles.Customer);

            await _signInManager.SignInAsync(user, isPersistent: false);

            return new RegisterResultDto
            {
                Succeeded = true,
                Role = Roles.Customer
            };
        }

        private async Task<string> GetUserRoleAsync(ApplicationUser user)
        {
            if (await _userManager.IsInRoleAsync(user, Roles.Admin))
                return Roles.Admin;

            return Roles.Customer;
        }
    }
}
