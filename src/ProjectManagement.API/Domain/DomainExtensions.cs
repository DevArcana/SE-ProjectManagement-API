using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.API.Domain.Issues;
using ProjectManagement.API.Domain.Projects;

namespace ProjectManagement.API.Domain
{
    public static class DomainExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddProjects(configuration);
            services.AddIssues(configuration);
            return services;
        }
    }
}