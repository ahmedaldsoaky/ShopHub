using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using myshop.DAL.Seeds.Dtos;
using myshop.Entities.Models;
using System.Text.Json;

namespace myshop.DAL.Seeds
{
    public class IdentitySeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles =
            {
            "Admin",
            "Customer"
        };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new IdentityRole(role));
                }
            }
        }

        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            var userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var path = Path.Combine(
                AppContext.BaseDirectory,
                "Seeds",
                "Json",
                "users.json");

            var json = await File.ReadAllTextAsync(path);

            var users = JsonSerializer.Deserialize<List<UserSeedDto>>(json);

            if (users is null)
                return;

            const string defaultPassword = "P@ssw0rd123";

            foreach (var item in users)
            {
                if (await userManager.FindByEmailAsync(item.Email) is not null)
                    continue;

                var user = new ApplicationUser
                {
                    FullName = item.FullName,
                    UserName = item.UserName,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, defaultPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, item.Role);
                }
            }
        }
    }
}
