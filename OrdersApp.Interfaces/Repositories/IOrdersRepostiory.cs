using Entities;

namespace OrdersApp.Interfaces.Repositories
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
        /// Возвращает объект заказа по его Id вместе со строками заказа
        /// </summary>
        public Task<Order?> GetOrderWithLinesById(Guid id);

        /// <summary>
        /// Возвращает объект заказа по его Id не включая строк заказа
        /// </summary>
        public Task<Order?> GetOrderById(Guid id);

        /// <summary>
        /// Обновляет существующий заказ
        /// </summary>
        /// <returns>Возвращает объект измененного заказа</returns>
        public Task<Order> UpdateOrder(Order order);
    }
}
