using Microsoft.Extensions.DependencyInjection;
using OrdersApp.Interfaces.Validators;
using OrdersApp.Implementations.Validators;

namespace OrdersApp.Implementations.DI
{
    public static class ValidationDependencyInjectionExtensions
    {
        public static IServiceCollection AddOrdersAppValidation(this IServiceCollection services)
        {
            services.AddScoped<IOrderCreateValidator, OrderCreateValidator>();
            services.AddScoped<IOrderDeleteValidator, OrderDeleteValidator>();
            services.AddScoped<IOrderUpdateValidator, OrderUpdateValidator>();
            return services;
        }
    }
}
