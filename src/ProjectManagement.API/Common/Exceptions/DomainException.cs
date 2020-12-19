using System;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.API.Common.Exceptions
{
    public class DomainException : Exception
    {
        public string Description { get; }
        
        public DomainException(string message, string description) : base(message)
        {
            Description = description;
        }
    }

    public class DomainProblemDetails : ProblemDetails
    {
        public static void Map(ProblemDetailsOptions setup)
        {
            setup.Map<DomainException>(exception => new DomainProblemDetails
            {
                Title = exception.Message,
                Detail = exception.Description,
                Status = StatusCodes.Status400BadRequest
            });
        }
    }
}