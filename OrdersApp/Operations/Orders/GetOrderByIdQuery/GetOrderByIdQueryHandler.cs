using Entities;
using MediatR;
using OrdersApp.Interfaces.Repositories;
using Utility.DTOs;
using Utility.Exceptions;

namespace OrdersApp.Operations.Orders.GetOrderByIdQuery
{
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
                Order? result = await _ordersRepository.GetOrderWithLinesById(context.OrderId);

                if (result is null)
                    throw new NonExistingOrderException(context.OrderId);

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
