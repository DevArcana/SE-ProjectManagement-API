using System;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.API.Common.Exceptions
{
    public class PropertyValidationException : Exception
    {
        public string PropertyName { get; }
        public string Description { get; }
        
        public PropertyValidationException(string propertyName, string description) : base("Validation error has occured")
        {
            PropertyName = propertyName;
            Description = description;
        }
        
        public static void Map(ProblemDetailsOptions setup)
        {
            setup.Map<PropertyValidationException>(exception => new ValidationProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Errors =
                {
                    {exception.PropertyName, new [] { exception.Description }}
                }
            });
        }
    }
}