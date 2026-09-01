using myshop.BLL.Extensions.DependencyInjection;
using myshop.BLL.Interfaces;
using myshop.Web.Services;

namespace myshop.Web.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependances(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBusinessLogic(configuration);
            services.AddScoped<IFilePathesService, FilePathesService>();
            return services;
        }
    }
}
