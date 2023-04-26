using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersApp.Operations.Orders;
using Utility.DTOs;
using Utility.RequestOptions;

namespace OrdersApp.Controllers
{
    /// <summary>
    /// Контроллер обрабатывает запросы для работы с объектами заказа.
    /// В контроллере используется библиотека MediatR. Классы запросов и обработчиков для MediatR находятся в директории Operations
    /// 
    /// Для отправки ответа используются DTO-классы. DTO классы служат для удобной передачи данных в системе
    /// без включения логики обработки, логики схемы БД и т.д. DTO классы находятся в Utility.DTOs
    /// 
    /// 
    /// Для получения данных из тела запроса используются RequestOptions классы. Они лежат в Utility.RequestOptions.
    /// </summary>

    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IMediator _mediator;
        public OrdersController(ILogger<OrdersController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator= mediator;
        }

        /// <summary>
        /// Создает заказ. Параметры заказа хранятся в объекте класса OrderCreateOptions.
        /// Возвращает объект с информацией о созданном заказе.
        /// </summary>
        
        [HttpPost("orders")]
        public async Task<ActionResult> CreateNewOrder([FromBody] OrderCreateOptions orderCreateOptions)
        {
            try
            {
                OrderDto resultDto = await _mediator.Send(new CreateNewOrderQuery { OrderOptions = orderCreateOptions});
                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Удаляет существующий заказ по Id
        /// </summary>

        [HttpDelete("orders")]
        public async Task<ActionResult> DeleteOrderById([FromQuery]Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteOrderByIdQuery { OrderIdToDelete = id});
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получает существующий заказ по Id. Возвращает объект заказа.
        /// </summary>

        [HttpGet("orders")]
        public async Task<ActionResult> GetOrderById([FromQuery] Guid id)
        {
            try
            {
                OrderDto resultDto = await _mediator.Send(new GetOrderByIdQuery { OrderId = id});
                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновляет поля существующего заказа
        /// Параметры заказа хранятся в объекте класса OrderUpdateOptions
        /// </summary>

        [HttpPut("orders")]
        public async Task<ActionResult> UpdateOrderById([FromQuery] Guid id, [FromBody] OrderUpdateOptions orderUpdateOptions)
        {
            try
            {
                OrderDto resultDto = await _mediator.Send(
                    new UpdateOrderQuery { OrderIdToUpdate = id, OrderUpdateOptions = orderUpdateOptions}
                    );

                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
