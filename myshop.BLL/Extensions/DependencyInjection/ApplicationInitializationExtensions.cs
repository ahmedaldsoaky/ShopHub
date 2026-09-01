using myshop.DAL.Extensions.DependencyInjection;

namespace myshop.BLL.Extensions.DependencyInjection
{
    public static class ApplicationInitializationExtensions
    {
        public static async Task InitializeApplicationAsync(
            this IServiceProvider services)
        {
            await services.InitializeDataAccessAsync();
        }
    }
}
