
namespace Utility.Exceptions
{
    public class DeletingOrderNotAllowedByStatusException : Exception
    {
        public DeletingOrderNotAllowedByStatusException(string status):
            base($"Невозможно удалить заказ со статусом '{status}'")
        { }
    }
}
