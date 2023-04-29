using Entities;
using Microsoft.Extensions.Logging;
using OrdersApp.Interfaces.Repositories;
using OrdersApp.Interfaces.Validators;
using Utility.Exceptions;
using Utility.Types;

namespace OrdersApp.Implementations.Validators
{
    public class OrderCreateValidator : IOrderCreateValidator
    {
        private readonly IOrdersRepostiory _ordersRepostiory;
        private readonly ILogger<OrderCreateValidator> _logger;
        public OrderCreateValidator(IOrdersRepostiory ordersRepostiory, ILogger<OrderCreateValidator> logger)
        {
            _ordersRepostiory = ordersRepostiory;
            _logger = logger;
        }
        public async Task<ValidationResult> Validate(Order newOrder)
        {
            try
            {
                if ((await _ordersRepostiory.GetOrderById(newOrder.Id)) is not null)
                {
                    return new ValidationResult(false, new NotUniqueGuidException());
                }

                if (!newOrder.OrderLines.Any())
                {
                    return new ValidationResult(
                        false,
                        new CommonValidationException("Ошибка валидации. Не заданы строки заказа.")
                        );
                }

                foreach (var line in newOrder.OrderLines)
                {
                    if (line.Quantity <= 0)
                    {
                        return new ValidationResult(
                            false,
                            new CommonValidationException("Ошибка валидации. Количество товара должно быть больше нуля")
                            );
                    }
                }

                return new ValidationResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
