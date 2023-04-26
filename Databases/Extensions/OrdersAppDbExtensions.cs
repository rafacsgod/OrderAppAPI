using Databases.DbContexts;
using Microsoft.Extensions.DependencyInjection;

namespace Databases.Extensions
{
    public static class OrdersAppDbExtensions
    {
        public static IServiceCollection AddOrdersAppDb(this IServiceCollection services)
        {
            services.AddDbContext<OrdersAppDbContext>();
            return services;
        }
    }
}
