
namespace Utility.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<OrderLineDto> Lines { get; set; } = Enumerable.Empty<OrderLineDto>();
    }
}
