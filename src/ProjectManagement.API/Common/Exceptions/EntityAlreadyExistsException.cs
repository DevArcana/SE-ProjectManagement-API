namespace ProjectManagement.API.Common.Exceptions
{
    public class EntityAlreadyExistsException : DomainException
    {
        public EntityAlreadyExistsException(string message) : base(message)
        {
            
        }
    }
}