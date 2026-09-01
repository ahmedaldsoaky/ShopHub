using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using myshop.DAL.Context;
using myshop.Entities.Models;
using System.Text.Json;

namespace myshop.DAL.Seeds
{
    public class DataSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<ApplicationDbContext>();

            if (!await context.Categories.AnyAsync())
            {
                var path = Path.Combine(
                    AppContext.BaseDirectory,
                    "Seeds",
                    "Json",
                    "categories.json");

                var json = await File.ReadAllTextAsync(path);

                var categories = JsonSerializer.Deserialize<List<Category>>(json);

                Console.WriteLine($"Categories before: {await context.Categories.CountAsync()}");

                if (categories is not null)
                {

                    Console.WriteLine($"Loaded Categories: {categories?.Count}");

                    await context.Categories.AddRangeAsync(categories);
                    await context.SaveChangesAsync();

                    Console.WriteLine($"Categories after: {await context.Categories.CountAsync()}");
                }
            }

            if (!await context.Products.AnyAsync())
            {
                var path = Path.Combine(
                    AppContext.BaseDirectory,
                    "Seeds",
                    "Json",
                    "products.json");

                var json = await File.ReadAllTextAsync(path);

                var products = JsonSerializer.Deserialize<List<Product>>(json);

                if (products is not null)
                {
                    await context.Products.AddRangeAsync(products);
                    await context.SaveChangesAsync();
                }
            }

        }
    }
}
