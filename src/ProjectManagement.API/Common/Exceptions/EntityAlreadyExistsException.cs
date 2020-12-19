using System;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.API.Common.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public string Description { get; }
        public EntityAlreadyExistsException(string message, string description) : base(message)
        {
            Description = description;
        }
        
        public static void Map(ProblemDetailsOptions setup)
        {
            setup.Map<EntityAlreadyExistsException>(exception => new ProblemDetails
            {
                Title = exception.Message,
                Detail = exception.Description,
                Status = StatusCodes.Status400BadRequest
            });
        }
    }
}