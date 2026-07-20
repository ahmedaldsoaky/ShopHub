using Microsoft.AspNetCore.Identity;
using myshop.Entities.Models;

namespace myshop.Web.Seeds;

public static class IdentitySeeder
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
    public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
    {
        var userManager =
            serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var admin = await userManager.FindByEmailAsync("admin@gmail.com");

        if (admin is null)
        {
            admin = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                FullName = "System Admin"
            };

            await userManager.CreateAsync(admin, "Admin123");

            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}