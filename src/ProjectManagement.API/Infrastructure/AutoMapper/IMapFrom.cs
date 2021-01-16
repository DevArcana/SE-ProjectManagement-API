using AutoMapper;

namespace ProjectManagement.API.Infrastructure.AutoMapper
{
    public interface IMapFrom<T>
    {
        public void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}