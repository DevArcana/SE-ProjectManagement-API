using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.API.Application.AppServices;
using ProjectManagement.API.Application.Interfaces;

namespace ProjectManagement.API.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProjectsAppService, ProjectsAppService>();
            services.AddScoped<IIssuesAppService, IssuesAppService>();
            services.AddScoped<IUsersAppService, UsersAppService>();
            return services;
        }
    }
}