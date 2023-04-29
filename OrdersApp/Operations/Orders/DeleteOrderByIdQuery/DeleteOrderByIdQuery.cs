using MediatR;
namespace OrdersApp.Operations.Orders.DeleteOrderByIdQuery
{
    public class DeleteOrderByIdQuery : IRequest<Unit>
    {
        public Guid OrderIdToDelete { get; set; }
    }

}
