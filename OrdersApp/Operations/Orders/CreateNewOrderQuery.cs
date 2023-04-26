using Entities;
using MediatR;
using OrdersApp.Interfaces;
using Utility.DTOs;
using Utility.RequestOptions;
using Utility.Types;

namespace OrdersApp.Operations.Orders
{
    public class CreateNewOrderQuery : IRequest<OrderDto>
    {
        public OrderCreateOptions OrderOptions { get; set; }
    }

    public class CreateNewOrderQueryHandler : IRequestHandler<CreateNewOrderQuery, OrderDto>
    {
        private readonly IOrdersRepostiory _ordersRepository;
        private readonly ILogger<CreateNewOrderQueryHandler> _logger;
        public CreateNewOrderQueryHandler(
            IOrdersRepostiory ordersRepository,
            ILogger<CreateNewOrderQueryHandler> logger)
        {
            _ordersRepository = ordersRepository;   
            _logger = logger;
        }

        public async Task<OrderDto> Handle(CreateNewOrderQuery context, CancellationToken cancellationToken)
        {
            try
            {
                Order NewOrder = new Order
                {
                    Id = context.OrderOptions.Id,
                    Status = OrderStatuses.New,
                    Created = DateTime.UtcNow
                };

                List<OrderLine> NewOrderLines =
                    context.OrderOptions.Lines
                    .Select(l => new OrderLine { ProductId = l.Id, Quantity = l.Qty }).ToList();

                NewOrder.OrderLines = NewOrderLines;

                Order result =  await _ordersRepository.CreateNewOrder(NewOrder);

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
