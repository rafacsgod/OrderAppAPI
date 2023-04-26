using Entities;
using MediatR;
using OrdersApp.Interfaces;
using Utility.DTOs;
using Utility.Exceptions;
using Utility.RequestOptions;
using Utility.Types;

namespace OrdersApp.Operations.Orders
{
    public class UpdateOrderQuery : IRequest<OrderDto>
    {
        public Guid OrderIdToUpdate { get; set; }
        public OrderUpdateOptions OrderUpdateOptions { get; set; }
    }


    public class UpdateOrderQueryHandler : IRequestHandler<UpdateOrderQuery, OrderDto>
    {
        private readonly ILogger<UpdateOrderQueryHandler> _logger;
        private readonly IOrdersRepostiory _ordersRepository;
        public UpdateOrderQueryHandler(ILogger<UpdateOrderQueryHandler> logger, IOrdersRepostiory ordersRepository)
        {
            _logger = logger;
            _ordersRepository = ordersRepository;

        }

        public async Task<OrderDto> Handle(UpdateOrderQuery context, CancellationToken token)
        {
            try
            {
                Guid orderId = context.OrderIdToUpdate;
                string newStatus = context.OrderUpdateOptions.Status;

                //проверяем корректность статуса заказа
                switch (newStatus)
                {
                    case OrderStatuses.New:
                    case OrderStatuses.Paid:
                    case OrderStatuses.AwaitingPayment:
                    case OrderStatuses.Delivering:
                    case OrderStatuses.Delivered:
                    case OrderStatuses.Completed:
                        break;
                    default:
                        //выбрасываем исключение если статус не был правильно написан
                        throw new NonExistingStatusExceptions();
                }

                List<OrderLine> newOrderLines = context.OrderUpdateOptions.Lines
                    .Select(l => new OrderLine { ProductId = l.Id, Quantity = l.Qty }).ToList();

                Order result = await _ordersRepository.UpdateOrder(orderId, newStatus, newOrderLines);
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
