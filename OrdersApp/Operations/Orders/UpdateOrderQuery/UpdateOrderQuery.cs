using Entities;
using MediatR;
using OrdersApp.Interfaces.Repositories;
using Utility.DTOs;
using Utility.Exceptions;
using Utility.RequestOptions;
using Utility.Types;

namespace OrdersApp.Operations.Orders.UpdateOrderQuery
{
    public class UpdateOrderQuery : IRequest<OrderDto>
    {
        public Guid OrderIdToUpdate { get; set; }
        public OrderUpdateOptions OrderUpdateOptions { get; set; }
    }
}
