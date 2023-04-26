
using Utility.DTOs;

namespace Utility.RequestOptions
{
    public class OrderUpdateOptions
    {
        public string Status { get; set; }
        public List<OrderLineDto> Lines { get; set; }
    }
}
