using MediatR;
using OrdersApp.Interfaces.Repositories;
using OrdersApp.Interfaces.Validators;
using Utility.Types;
namespace OrdersApp.Operations.Orders.DeleteOrderByIdQuery
{
    public class DeleteOrderByIdQueryHandler : IRequestHandler<DeleteOrderByIdQuery, Unit>
    {
        private readonly IOrdersRepostiory _ordersRepository;
        private readonly ILogger<DeleteOrderByIdQueryHandler> _logger;
        private readonly IOrderDeleteValidator _orderDeleteValidator;
        public DeleteOrderByIdQueryHandler(
            IOrdersRepostiory ordersRepo, 
            ILogger<DeleteOrderByIdQueryHandler> logger,
            IOrderDeleteValidator orderDeleteValidator
            )
        {
            _ordersRepository = ordersRepo;
            _logger = logger;
            _orderDeleteValidator = orderDeleteValidator;
        }

        public async Task<Unit> Handle(DeleteOrderByIdQuery context, CancellationToken cancellationToken)
        {
            try
            {
                //Валидация
                ValidationResult validationResult = await _orderDeleteValidator.Validate(context.OrderIdToDelete);
                if (!validationResult.IsValid)
                    throw validationResult.Exception!;

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
