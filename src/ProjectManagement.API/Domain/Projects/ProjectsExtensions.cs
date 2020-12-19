using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.API.Domain.Projects.Interfaces;
using ProjectManagement.API.Domain.Projects.Services;

namespace ProjectManagement.API.Domain.Projects
{
    public static class ProjectsExtensions
    {
        public static IServiceCollection AddProjects(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProjectsService, ProjectsService>();
            return services;
        }
    }
}