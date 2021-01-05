using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.API.Domain.Issues.Interfaces;
using ProjectManagement.API.Domain.Issues.Services;

namespace ProjectManagement.API.Domain.Issues
{
    public static class IssuesExtensions
    {
        public static IServiceCollection AddIssues(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IIssuesService, IssuesService>();
            return services;
        }
    }
}