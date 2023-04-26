using Databases.DbContexts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrdersApp.Interfaces;
using Utility.Exceptions;
using Utility.Types;

namespace OrdersApp.Implementations
{
    public class OrdersRepository : IOrdersRepostiory
    {
        private readonly OrdersAppDbContext _orderAppDbContext;
        private readonly ILogger<OrdersRepository> _logger;
        public OrdersRepository(OrdersAppDbContext dbContext, ILogger<OrdersRepository> logger)
        {
            _orderAppDbContext = dbContext;
            _logger = logger;
        }

        public async Task<Order> CreateNewOrder(Order newOrder)
        {
            try
            {
                if (_orderAppDbContext.Orders.Any(o => o.Id == newOrder.Id))
                    throw new NotUniqueGuidException();

                if (!newOrder.OrderLines.Any())
                    throw new NoOrderLinesException();

                foreach (var line in newOrder.OrderLines)
                {
                    if (line.Quantity <= 0)
                        throw new InvalidQuantityValueException();
                }

                //вставка в БД
                await _orderAppDbContext.AddAsync(newOrder);
                _orderAppDbContext.SaveChanges();

                return newOrder;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteOrderById(Guid id)
        {
            try
            {
                if ( !(await _orderAppDbContext.Orders.AnyAsync(o => o.Id == id)) )
                    throw new NonExistingOrderException(id);

                Order order = await _orderAppDbContext.Orders.FirstAsync(o => o.Id == id);

                switch (order.Status)
                {
                    case OrderStatuses.Delivering:
                    case OrderStatuses.Delivered:
                    case OrderStatuses.Completed:
                        throw new DeletingOrderNotAllowedByStatusException(order.Status);
                        
                    default:
                        break;
                }

                _orderAppDbContext.Orders.Remove(order);
                _orderAppDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            try
            {
                if ( ! (await _orderAppDbContext.Orders.AnyAsync(o => o.Id == id)))
                    throw new NonExistingOrderException(id);

                Order order = await _orderAppDbContext.Orders.Include(o => o.OrderLines).FirstAsync(o => o.Id == id);
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Order> UpdateOrder(Guid id, string newStatus, List<OrderLine> OrderLines)
        {
            try
            {
                Order? order = await _orderAppDbContext.Orders.Include(o => o.OrderLines).FirstOrDefaultAsync(o => o.Id == id);

                if (order is null)
                    throw new NonExistingOrderException(id);

                if (order.Status != OrderStatuses.New && order.Status != OrderStatuses.AwaitingPayment)
                    throw new Exception($"Невозможно отредактировать заказ со статусом {order.Status}");

                order.Status = newStatus;
                order.OrderLines.Clear();
                order.OrderLines.AddRange(OrderLines);

                await _orderAppDbContext.SaveChangesAsync();

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
