using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Core
{
    public static class CoreInstaller
    {
		public static IServiceCollection AddCore(this IServiceCollection services)
		{
			var sp = services.BuildServiceProvider();
			Configs configs = sp.GetService<IOptions<Configs>>().Value;
			services.AddDbContext<OnlineShopDbContext>(options =>
			{
				options.UseSqlServer(configs.DBConnection);
			});

			return services;
		}
	}
}
