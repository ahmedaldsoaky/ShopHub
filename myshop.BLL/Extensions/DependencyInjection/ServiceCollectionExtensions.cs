using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using myshop.BLL.Interfaces;
using myshop.BLL.Services;
using myshop.DAL.Extensions.DependencyInjection;

namespace myshop.BLL.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogic(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDataAccess(configuration);

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICartService, CartService>();

            services.AddScoped<ICheckoutService, CheckoutService>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IImageValidationService, ImageValidationService>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
