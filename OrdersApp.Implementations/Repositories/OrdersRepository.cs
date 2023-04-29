using Databases.DbContexts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrdersApp.Interfaces.Repositories;
using Utility.Exceptions;
using Utility.Types;

namespace OrdersApp.Implementations.Repositories
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
                Order orderToDelete = await _orderAppDbContext.Orders.FirstAsync(o => o.Id == id);
                _orderAppDbContext.Orders.Remove(orderToDelete);
                _orderAppDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<Order?> GetOrderById(Guid id)
        {
            try
            {
                Order? order = await _orderAppDbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Order?> GetOrderWithLinesById(Guid id)
        {
            try
            {
                Order? order = await _orderAppDbContext.Orders.Include(o => o.OrderLines).FirstOrDefaultAsync(o => o.Id == id);
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            try
            {
                //order - заказ с информацией, на которую нужно обновить
                //orderToUpdate - заказ из БД, которую будем обновлять

                Order orderToUpdate = 
                    await _orderAppDbContext.Orders
                    .Include(o => o.OrderLines)
                    .FirstAsync(o => o.Id == order.Id);

                orderToUpdate.Status = order.Status;
                orderToUpdate.OrderLines.Clear();
                orderToUpdate.OrderLines.AddRange(order.OrderLines);

                await _orderAppDbContext.SaveChangesAsync();

                //возвращаем обновленный заказ
                return orderToUpdate;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
