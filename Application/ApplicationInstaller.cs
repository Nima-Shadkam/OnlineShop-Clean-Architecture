
using Infrastructure.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationInstaller
    {
        public static IServiceCollection AddApplicaiton(this IServiceCollection services)
        {
       
            #region Products
            services.AddScoped<IProductService, ProductService>();

            #endregion

            return services;
        }
    }
}
