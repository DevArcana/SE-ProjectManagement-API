using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.API.Domain.Issues;
using ProjectManagement.API.Domain.Projects;
using ProjectManagement.API.Domain.Users;

namespace ProjectManagement.API.Domain
{
    public static class DomainExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddProjects(configuration);
            services.AddIssues(configuration);
            services.AddUsers(configuration);
            return services;
        }
    }
}