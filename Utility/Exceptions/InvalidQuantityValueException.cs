

namespace Utility.Exceptions
{
    public class InvalidQuantityValueException : Exception
    {
        public InvalidQuantityValueException() 
            :base("Количество товара должно быть числом больше нуля")
        { }
    }
}
