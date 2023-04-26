using Utility.DTOs;
namespace Utility.RequestOptions
{

    public class OrderCreateOptions
    {
        public Guid Id { get; set; }
        public List<OrderLineDto> Lines { get; set; }
    }
}
