using AutoMapper;
using ProjectManagement.API.Domain.Issues.Entities;
using ProjectManagement.API.Infrastructure.AutoMapper;

namespace ProjectManagement.API.Application.Models
{
    public class IssueDto : IMapFrom<Issue>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Closed { get; set; }
        public Status Status { get; set; }
        public string Assignee { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Issue, IssueDto>()
                .ForMember(dest => dest.Assignee,
                    opt => opt.MapFrom(s=>s.Assignee.UserName));
        }
    }
}