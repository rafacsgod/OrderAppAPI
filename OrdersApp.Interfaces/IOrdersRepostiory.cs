using Entities;

namespace OrdersApp.Interfaces
{
    public interface IOrdersRepostiory
    {
        /// <summary>
        /// Создает новый заказ
        /// </summary>
        /// <param name="newOrder"></param>
        /// <returns>Возвращает созданный объект заказа</returns>
        public Task<Order> CreateNewOrder(Order newOrder);

        /// <summary>
        /// Удаляет заказ по Id 
        /// </summary>
        public Task DeleteOrderById(Guid id);

        /// <summary>
        /// ВОзвращает объект заказа по его Id
        /// </summary>
        public Task<Order> GetOrderById(Guid id);

        /// <summary>
        /// Обновляет существующий заказ
        /// </summary>
        /// <returns>Возвращает объект измененного заказа</returns>
        public Task<Order> UpdateOrder(Guid id, string newStatus, List<OrderLine> OrderLines);
    }
}
