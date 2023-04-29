using Utility.Types;
namespace OrdersApp.Interfaces.Validators
{
    public interface IOrderDeleteValidator
    {
        public Task<ValidationResult> Validate(Guid id);
    }
}
