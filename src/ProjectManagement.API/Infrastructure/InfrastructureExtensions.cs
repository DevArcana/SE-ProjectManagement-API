using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using ProjectManagement.API.Infrastructure.Persistence;

namespace ProjectManagement.API.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(Startup));
            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(GetHerokuDbConnection() ?? configuration.GetConnectionString("Database"));
            });
            
            return services;
        }
        
        private static string GetHerokuDbConnection()
        {
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            if (databaseUrl == null)
            {
                return null;
            }
            
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require
            };

            return builder.ToString();
        }
    }
}