namespace ProjectManagement.API.Common.Exceptions
{
    public class ValidationException : DomainException
    {
        public ValidationException(string message) : base(message)
        {
            
        }
    }
}