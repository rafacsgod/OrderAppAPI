using Entities;
using MediatR;
using OrdersApp.Interfaces;
using Utility.DTOs;

namespace OrdersApp.Operations.Orders
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public Guid OrderId { get; set; }
    }

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IOrdersRepostiory _ordersRepository;
        private readonly ILogger<GetOrderByIdQueryHandler> _logger;
        public GetOrderByIdQueryHandler(IOrdersRepostiory ordersRepository, ILogger<GetOrderByIdQueryHandler> logger)
        {
            _ordersRepository = ordersRepository;
            _logger = logger;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery context, CancellationToken cancellationToken)
        {
            try
            {
                Order result = await _ordersRepository.GetOrderById(context.OrderId);

                OrderDto resultDto = new OrderDto
                {
                    Id = result.Id,
                    Status = result.Status,
                    Created = result.Created,
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
