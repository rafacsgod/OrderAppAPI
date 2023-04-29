using Utility.Types;
using Entities;
namespace OrdersApp.Interfaces.Validators
{
    public interface IOrderUpdateValidator
    {
        public Task<ValidationResult> Validate(Order orderToUpdate);
    }
}
