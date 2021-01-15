using ProjectManagement.API.Domain.Projects.Entities;
using ProjectManagement.API.Infrastructure.AutoMapper;

namespace ProjectManagement.API.Application.Models
{
    public class CollaboratorDto : IMapFrom<UserProjectAccess>
    {
        public string UserId { get; set; }
    }
}