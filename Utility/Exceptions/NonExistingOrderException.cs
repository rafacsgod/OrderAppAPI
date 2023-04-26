
namespace Utility.Exceptions
{
    public class NonExistingOrderException : Exception
    {
        public NonExistingOrderException(Guid id)
            :base($"Заказа с id: {id} не существует")
        { }
    }
}
