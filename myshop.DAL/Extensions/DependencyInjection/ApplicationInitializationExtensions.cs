using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using myshop.DAL.Context;
using myshop.DAL.Seeds;

namespace myshop.DAL.Extensions.DependencyInjection
{
    public static class ApplicationInitializationExtensions
    {
        public static async Task InitializeDataAccessAsync(
            this IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var context = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            await context.Database.MigrateAsync();

            await IdentitySeeder.SeedRolesAsync(scope.ServiceProvider);
            await IdentitySeeder.SeedUsersAsync(scope.ServiceProvider);
            await DataSeeder.SeedAsync(scope.ServiceProvider);
        }
    }
}
