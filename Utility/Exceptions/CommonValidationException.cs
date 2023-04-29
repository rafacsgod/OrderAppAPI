
namespace Utility.Exceptions
{
    public class CommonValidationException : Exception
    {
        public CommonValidationException():
            base("Ошибка валидации. Проверьте корректность содержимого запроса")
        { }
        
        public CommonValidationException(string message):
            base(message)
        {}
    }
}
