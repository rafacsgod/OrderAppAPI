using Entities;
using MediatR;
using OrdersApp.Interfaces.Repositories;
using Utility.DTOs;

namespace OrdersApp.Operations.Orders.GetOrderByIdQuery
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public Guid OrderId { get; set; }
    }

}
