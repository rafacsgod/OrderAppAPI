using Databases.DbContexts;
using Entities;
using Microsoft.Extensions.Logging;
using OrdersApp.Interfaces.Repositories;
using OrdersApp.Interfaces.Validators;
using Utility.Exceptions;
using Utility.Types;
namespace OrdersApp.Implementations.Validators
{
    public class OrderDeleteValidator : IOrderDeleteValidator
    {
        private readonly IOrdersRepostiory _orderRepository;
        private readonly ILogger<OrderDeleteValidator> _logger;
        public OrderDeleteValidator(
            IOrdersRepostiory orderRepository,
            ILogger<OrderDeleteValidator> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task<ValidationResult> Validate(Guid id)
        {
            try
            {
                Order? orderToDelete = await _orderRepository.GetOrderById(id);

                if (orderToDelete is null)
                    return new ValidationResult(false, new NonExistingOrderException(id));

                switch (orderToDelete.Status)
                {
                    case OrderStatuses.Delivering:
                    case OrderStatuses.Delivered:
                    case OrderStatuses.Completed:
                        return new ValidationResult(
                            false,
                            new DeletingOrderNotAllowedByStatusException(orderToDelete.Status)
                            );
                    default:
                        break;
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
