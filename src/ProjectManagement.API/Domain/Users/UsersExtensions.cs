using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.API.Domain.Users.Interfaces;
using ProjectManagement.API.Domain.Users.Services;

namespace ProjectManagement.API.Domain.Users
{
    public static class UsersExtensions
    {
        public static IServiceCollection AddUsers(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddScoped<IUsersService, UsersService>(); 
            return services;
        }
        
    }
}