using ProjectManagement.API.Domain.Users.Entities;
using ProjectManagement.API.Infrastructure.AutoMapper;

namespace ProjectManagement.API.Application.Models
{
    public class AssignableUserDto : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }
    }
}