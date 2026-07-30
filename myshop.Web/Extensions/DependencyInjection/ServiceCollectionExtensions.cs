using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using myshop.BLL.Interfaces;
using myshop.BLL.Services;
using myshop.DAL.Context;
using myshop.DAL.Interfaces;
using myshop.DAL.Repositories;
using myshop.DAL.UnitOfWork;
using myshop.Entities.Models;
using myshop.Web.Services;
using myshop.Web.Services.IServices;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
                ));


            services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(4);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 4;
                }
                ).AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            
            services.AddScoped<IUserService, UserService>();
            
            services.AddScoped<IImageService, ImageService>();
            return services;
        }
    }
}
