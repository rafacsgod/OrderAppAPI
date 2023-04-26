
namespace Utility.DTOs
{
    public class OrderLineDto
    {
        /// <summary>
        /// Id товара
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Количество товара
        /// </summary>
        public int Qty { get; set; }
    }
}
