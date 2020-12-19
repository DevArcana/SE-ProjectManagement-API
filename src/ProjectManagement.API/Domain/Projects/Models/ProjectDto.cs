using ProjectManagement.API.Domain.Projects.Entities;
using ProjectManagement.API.Infrastructure.AutoMapper;

namespace ProjectManagement.API.Domain.Projects.Models
{
    public class ProjectDto : IMapFrom<Project>
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}