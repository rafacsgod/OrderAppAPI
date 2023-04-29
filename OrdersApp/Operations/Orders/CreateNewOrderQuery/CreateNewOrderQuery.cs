
using MediatR;
using Utility.DTOs;
using Utility.RequestOptions;

namespace OrdersApp.Operations.Orders.CreateNewOrderQuery
{
    public class CreateNewOrderQuery : IRequest<OrderDto>
    {
        public OrderCreateOptions OrderOptions { get; set; }
    }
}
