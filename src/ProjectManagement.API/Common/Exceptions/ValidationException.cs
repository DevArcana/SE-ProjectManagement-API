using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.API.Common.Exceptions
{
    public class ValidationException : DomainException
    {
        public ValidationException(string propertyName, string message) : base(propertyName, message)
        {
            
        }
    }
}