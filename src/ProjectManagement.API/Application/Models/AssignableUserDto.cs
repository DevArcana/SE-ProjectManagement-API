using AutoMapper;
using ProjectManagement.API.Domain.Users.Entities;
using ProjectManagement.API.Infrastructure.AutoMapper;

namespace ProjectManagement.API.Application.Models
{
    public class AssignableUserDto : IMapFrom<ApplicationUser>
    {
        public string Username { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, AssignableUserDto>()
                .ForMember(dest => dest.Username, 
                    opt => opt.MapFrom(s=>s.UserName));
        }
    }
}