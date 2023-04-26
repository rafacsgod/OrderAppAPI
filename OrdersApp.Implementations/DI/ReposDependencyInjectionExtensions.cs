using Microsoft.Extensions.DependencyInjection;
using OrdersApp.Interfaces;
using OrdersApp.Implementations;

namespace OrdersApp.DI
{
    /// <summary>
    /// Внедрение репозиториев в сервисы
    /// </summary>
    public static class ReposDependencyInjectionExtensions
    {
        public static IServiceCollection AddOrdersAppRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrdersRepostiory, OrdersRepository>();
            return services;
        }
    }
}
