using Microsoft.Extensions.DependencyInjection;
using OrdersApp.Implementations.Repositories;
using OrdersApp.Interfaces.Repositories;

namespace OrdersApp.Implementations.DI
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



