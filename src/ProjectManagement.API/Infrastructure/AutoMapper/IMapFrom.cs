using AutoMapper;

namespace ProjectManagement.API.Infrastructure.AutoMapper
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}