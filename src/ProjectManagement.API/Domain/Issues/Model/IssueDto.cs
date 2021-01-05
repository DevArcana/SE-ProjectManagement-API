using ProjectManagement.API.Domain.Issues.Entities;
using ProjectManagement.API.Infrastructure.AutoMapper;

namespace ProjectManagement.API.Domain.Issues.Model
{
    public class IssueDto : IMapFrom<Issue>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}