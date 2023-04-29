
namespace Utility.Types
{
    public class ValidationResult
    {
        public bool IsValid { get; }
        public Exception? Exception { get; }

        public ValidationResult(bool result, Exception? exception = null)
        {
            IsValid = result;
            Exception = exception;
        }
    }
}
