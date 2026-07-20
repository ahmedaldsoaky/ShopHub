using myshop.BLL.Interfaces;
using myshop.BLL.Services;
using myshop.DAL.Interfaces;
using myshop.DAL.Repositories;
using myshop.DAL.UnitOfWork;
using myshop.Web.Services;
using myshop.Web.Services.IServices;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            
            services.AddScoped<IImageService, ImageService>();
            return services;
        }
    }
}
