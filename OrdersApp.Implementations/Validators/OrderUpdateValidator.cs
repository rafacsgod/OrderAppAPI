using Entities;
using Microsoft.Extensions.Logging;
using OrdersApp.Interfaces.Repositories;
using OrdersApp.Interfaces.Validators;
using Utility.Exceptions;
using Utility.Types;

namespace OrdersApp.Implementations.Validators
{
    public class OrderUpdateValidator : IOrderUpdateValidator
    {
        private readonly IOrdersRepostiory _ordersRepository;
        private readonly ILogger<OrderUpdateValidator> _logger;
        public OrderUpdateValidator(IOrdersRepostiory ordersRepository, ILogger<OrderUpdateValidator> logger)
        {
            _ordersRepository = ordersRepository;
            _logger = logger;
        }

        public async Task<ValidationResult> Validate(Order orderToUpdate)
        {
            try
            {
                //Проверка статуса
                switch (orderToUpdate.Status)
                {
                    case OrderStatuses.New:
                    case OrderStatuses.Paid:
                    case OrderStatuses.AwaitingPayment:
                    case OrderStatuses.Delivering:
                    case OrderStatuses.Delivered:
                    case OrderStatuses.Completed:
                        break;
                    default:
                        return new ValidationResult(
                            false,
                            new CommonValidationException($"Ошибка валидации. Некорректный статус '{orderToUpdate.Status}'")
                        );
                }

                //Проверка, удовлетворяют ли новые строки заказа всем условиям
                if (!orderToUpdate.OrderLines.Any())
                    return new ValidationResult
                        (
                            false, 
                            new CommonValidationException("Ошибка валидации. Заказ не может быть пустым")
                        );

                foreach (var orderLine in orderToUpdate.OrderLines)
                {
                    if (orderLine.Quantity <= 0)
                    {
                        return new ValidationResult(
                            false,
                            new CommonValidationException("Ошибка валидации. Количество товара в заказе должно быть больше 0")
                            );
                    }
                }


                Order? order = await _ordersRepository.GetOrderWithLinesById(orderToUpdate.Id);

                //Проверка, существует ли обновляемая запись в хранилище
                if (order is null)
                    return new ValidationResult(
                        false,
                        new NonExistingOrderException(orderToUpdate.Id)
                        );

                //Проверка, доступно ли обновление для записи с этим статусом
                if (order.Status != OrderStatuses.New && order.Status != OrderStatuses.AwaitingPayment)
                    return new ValidationResult(
                        false,
                        new CommonValidationException($"Ошибка валидации. Невозможно отредактировать заказ со статусом {order.Status}")
                        );

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
