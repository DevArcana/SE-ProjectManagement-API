using ProjectManagement.API.Domain.Users.Entities;
using ProjectManagement.API.Infrastructure.AutoMapper;

namespace ProjectManagement.API.Domain.Users.Models
{
    public class UserDto : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
    }
}