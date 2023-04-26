
namespace Utility.Exceptions
{
    public class NotUniqueGuidException : Exception
    {
        public NotUniqueGuidException()
            :base("Заказ с заданным Guid уже существует")
        { }
    }
}
