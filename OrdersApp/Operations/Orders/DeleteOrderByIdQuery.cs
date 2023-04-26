using MediatR;
using OrdersApp.Interfaces;
namespace OrdersApp.Operations.Orders
{
    public class DeleteOrderByIdQuery : IRequest<Unit>
    {
        public Guid OrderIdToDelete { get; set; }
    }

    public class DeleteOrderByIdQueryHandler : IRequestHandler<DeleteOrderByIdQuery, Unit>
    {
        private readonly IOrdersRepostiory _ordersRepository;
        private readonly ILogger<DeleteOrderByIdQueryHandler> _logger;
        public DeleteOrderByIdQueryHandler(IOrdersRepostiory ordersRepo, ILogger<DeleteOrderByIdQueryHandler> logger)
        {
            _ordersRepository = ordersRepo;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteOrderByIdQuery context, CancellationToken cancellationToken)
        {
            try
            {
                await _ordersRepository.DeleteOrderById(context.OrderIdToDelete);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
