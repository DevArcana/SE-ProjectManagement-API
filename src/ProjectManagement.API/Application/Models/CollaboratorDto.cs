using AutoMapper;
using ProjectManagement.API.Domain.Projects.Entities;
using ProjectManagement.API.Infrastructure.AutoMapper;

namespace ProjectManagement.API.Application.Models
{
    public class CollaboratorDto : IMapFrom<UserProjectAccess>
    {
        public string UserName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserProjectAccess, CollaboratorDto>()
                .ForMember(dest => dest.UserName, 
                    opt => opt.MapFrom(s=>s.User.UserName));
        }
    }
}