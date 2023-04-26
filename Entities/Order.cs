
namespace Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public List<OrderLine> OrderLines { get; set; }
    }
}
