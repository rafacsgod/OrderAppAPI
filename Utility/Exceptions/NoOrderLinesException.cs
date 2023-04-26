

namespace Utility.Exceptions
{
    public class NoOrderLinesException : Exception
    {
        public NoOrderLinesException()
            :base("Не заданы строки для заказа")
        { }
    }
}
