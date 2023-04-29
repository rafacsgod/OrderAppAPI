using Entities;
using MediatR;
using OrdersApp.Interfaces.Repositories;
using OrdersApp.Interfaces.Validators;
using Utility.DTOs;
using Utility.Types;

namespace OrdersApp.Operations.Orders.CreateNewOrderQuery
{
    public class CreateNewOrderQueryHandler : IRequestHandler<CreateNewOrderQuery, OrderDto>
    {
        private readonly IOrdersRepostiory _ordersRepository;
        private readonly ILogger<CreateNewOrderQueryHandler> _logger;
        private readonly IOrderCreateValidator _orderCreateValidator;
        public CreateNewOrderQueryHandler(
            IOrdersRepostiory ordersRepository,
            ILogger<CreateNewOrderQueryHandler> logger,
            IOrderCreateValidator orderCreateValidator)
        {
            _ordersRepository = ordersRepository;
            _logger = logger;
            _orderCreateValidator = orderCreateValidator;
        }

        public async Task<OrderDto> Handle(CreateNewOrderQuery context, CancellationToken cancellationToken)
        {
            try
            {
                Order newOrder = new Order
                {
                    Id = context.OrderOptions.Id,
                    Status = OrderStatuses.New,
                    Created = DateTime.UtcNow
                };

                List<OrderLine> newOrderLines =
                    context.OrderOptions.Lines
                    .Select(l => new OrderLine { ProductId = l.Id, Quantity = l.Qty }).ToList();

                newOrder.OrderLines = newOrderLines;

                //Валидация
                ValidationResult validationResult = await _orderCreateValidator.Validate(newOrder);

                if (!validationResult.IsValid)
                    throw validationResult.Exception!;

                //Если все хорошо, сохраняем в репозиторий
                Order result = await _ordersRepository.CreateNewOrder(newOrder);

                //Возвращаем результат
                OrderDto resultDto = new OrderDto
                {
                    Id = result.Id,
                    Status = result.Status,
                    Created = result.Created,
                    Lines = result.OrderLines
                    .Select(l => new OrderLineDto { Id = l.ProductId, Qty = l.Quantity })
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
