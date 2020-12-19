using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.API.Common.Exceptions
{
    public class EntityAlreadyExistsException : DomainException
    {
        public EntityAlreadyExistsException(string message) : base("already exists", message)
        {
            
        }
    }
    
    public class EntityAlreadyExistsProblemDetails : ProblemDetails
    {
        public static void Map(ProblemDetailsOptions setup)
        {
            setup.Map<EntityAlreadyExistsException>(exception => new EntityAlreadyExistsProblemDetails
            {
                Title = exception.Message,
                Detail = exception.Description,
                Status = StatusCodes.Status400BadRequest
            });
        }
    }
}