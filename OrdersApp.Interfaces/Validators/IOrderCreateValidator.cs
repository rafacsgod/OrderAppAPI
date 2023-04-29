using Entities;
using Utility.Types;

namespace OrdersApp.Interfaces.Validators
{
    public interface IOrderCreateValidator
    {
        public Task<ValidationResult> Validate(Order newOrder);
    }
}
