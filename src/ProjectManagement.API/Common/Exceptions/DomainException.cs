using System;

namespace ProjectManagement.API.Common.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
            
        }
    }
}