using Entities;
using MediatR;
using OrdersApp.Interfaces.Repositories;
using OrdersApp.Interfaces.Validators;
using Utility.DTOs;
using Utility.Types;

namespace OrdersApp.Operations.Orders.UpdateOrderQuery
{
    public class UpdateOrderQueryHandler : IRequestHandler<UpdateOrderQuery, OrderDto>
    {
        private readonly ILogger<UpdateOrderQueryHandler> _logger;
        private readonly IOrdersRepostiory _ordersRepository;
        private readonly IOrderUpdateValidator _orderUpdateValidator;
        public UpdateOrderQueryHandler(
            ILogger<UpdateOrderQueryHandler> logger, 
            IOrdersRepostiory ordersRepository,
            IOrderUpdateValidator orderUpdateValidator
            )
        {
            _logger = logger;
            _ordersRepository = ordersRepository;
            _orderUpdateValidator = orderUpdateValidator;
        }

        public async Task<OrderDto> Handle(UpdateOrderQuery context, CancellationToken token)
        {
            try
            {

                Order orderToUpdate = new Order
                {
                    Id = context.OrderIdToUpdate,
                    Status = context.OrderUpdateOptions.Status,
                    OrderLines = context.OrderUpdateOptions.Lines
                        .Select(line => new OrderLine
                        {
                            ProductId = line.Id,
                            Quantity = line.Qty
                        })
                        .ToList()
                };

                ValidationResult validationResult = await _orderUpdateValidator.Validate(orderToUpdate);
                if (!validationResult.IsValid)
                    throw validationResult.Exception!;

                Order result = await _ordersRepository.UpdateOrder(orderToUpdate);
                OrderDto resultDto = new OrderDto
                {
                    Id = result.Id,
                    Status = result.Status,
                    Lines = result.OrderLines.Select(l => new OrderLineDto { Id = l.ProductId, Qty = l.Quantity })
                };

                return resultDto;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
